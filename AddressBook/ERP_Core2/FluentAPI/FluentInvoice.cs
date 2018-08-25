using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.Interfaces;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_Core2.AccountPayableDomain.AccountsPayableModule;

namespace ERP_Core2.FluentAPI
{
    public class FluentInvoice : AbstractErrorHandling, IInvoice
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;

        public FluentInvoice() { }


        public IInvoice Apply()
        {
            if (processStatus == CreateProcessStatus.Inserted || processStatus == CreateProcessStatus.Updated || processStatus == CreateProcessStatus.Deleted)
            { unitOfWork.CommitChanges(); }
            return this as IInvoice;
        }
        public IInvoice CreateInvoice(InvoiceView invoiceView)
        {

            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.invoiceRepository.CreateInvoiceByView(invoiceView));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IInvoice;

        }
        public IInvoice MergeWithInvoiceNumber(ref InvoiceView invoiceView)
        {

            string invoiceNumber = invoiceView.InvoiceNumber;

            Task<Invoice> viewTask = Task.Run(() => unitOfWork.invoiceRepository.GetInvoiceByInvoiceNumber(invoiceNumber));
            Task.WaitAll(viewTask);

            //TODO applicationFactory needs to have a merge feature created
            invoiceView.InvoiceId = viewTask.Result.InvoiceId;
            invoiceView.InvoiceNumber = viewTask.Result.InvoiceNumber;
            invoiceView.InvoiceDate = viewTask.Result.InvoiceDate;
            invoiceView.Amount = viewTask.Result.Amount;
            invoiceView.CustomerId = viewTask.Result.Customer.CustomerId;
            invoiceView.CustomerName = viewTask.Result.Customer.AddressBook.Name;
            invoiceView.Description = viewTask.Result.Description;
            invoiceView.TaxAmount = viewTask.Result.TaxAmount;
            invoiceView.PaymentDueDate = viewTask.Result.PaymentDueDate;
            invoiceView.DiscountAmount = viewTask.Result.DiscountAmount;
            invoiceView.PaymentTerms = viewTask.Result.PaymentTerms;
            invoiceView.CompanyId = viewTask.Result.Company.CompanyId;
            invoiceView.CompanyName = viewTask.Result.Company.CompanyName;
            invoiceView.CompanyStreet = viewTask.Result.Company.CompanyStreet;
            invoiceView.CompanyCity = viewTask.Result.Company.CompanyCity;
            invoiceView.CompanyZipcode = viewTask.Result.Company.CompanyZipcode;
            invoiceView.DiscountDueDate = viewTask.Result.DiscountDueDate;
            invoiceView.FreightCost = viewTask.Result.FreightCost;

            return this as IInvoice;
        }

        public IQuery Query()
        {
            FluentQuery query = new FluentQuery();
            return query as IQuery;
        }

    }
}
