using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
 
    public class FluentInvoiceQuery : AbstractErrorHandling, IInvoiceQuery
    {
        public UnitOfWork _unitOfWork = null;
        public FluentInvoiceQuery() { }
        public FluentInvoiceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public List<InvoiceFlatView> GetInvoicesByDate(DateTime startInvoiceDate, DateTime endInvoiceDate)
        {
            return _unitOfWork.invoiceRepository.GetInvoicesByDate(startInvoiceDate, endInvoiceDate);
        }

    }
    public class FluentInvoice : AbstractErrorHandling, IInvoice
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;

        private FluentInvoiceQuery _query = null;

        public FluentInvoice() { }

        public IInvoiceQuery Query()
        {
            if (_query == null) { _query = new FluentInvoiceQuery(unitOfWork); }

            return _query as IInvoiceQuery;

        }



        public IInvoice Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
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
            invoiceView.CustomerName = viewTask.Result.Customer.Address.Name;
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

      

    }
}
