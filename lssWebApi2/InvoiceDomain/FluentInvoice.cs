using lssWebApi2.AbstractFactory;
using lssWebApi2.AutoMapper;
using lssWebApi2.Interfaces;
using lssWebApi2.InvoicesDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
 
 
    public class FluentInvoice : AbstractErrorHandling, IFluentInvoice
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        public CreateProcessStatus processStatus;

        private FluentInvoiceQuery _query = null;

        public FluentInvoice() { }

        public IFluentInvoiceQuery Query()
        {
            if (_query == null) { _query = new FluentInvoiceQuery(unitOfWork); }

            return _query as IFluentInvoiceQuery;

        }
        private Invoice MapToEntity(InvoiceView inputObject)
        {
            Mapper mapper = new Mapper();
            Invoice outObject = mapper.Map<Invoice>(inputObject);
            return outObject;
        }
        public IFluentInvoice CreateInvoiceByView(InvoiceView invoiceView)
        {
            try
            {
                Invoice invoice = new Invoice();
                invoice =  MapToEntity(invoiceView);

                Task<Invoice> invoiceTask = Task.Run(async () => await unitOfWork.invoiceRepository.GetEntityByInvoiceDocument(invoice.InvoiceDocument));
                Task.WaitAll(invoiceTask);

                if (invoiceTask.Result == null)
                {
                    AddInvoice(invoice);
                    return this as IFluentInvoice;
                }
                processStatus=CreateProcessStatus.AlreadyExists;
                return this as IFluentInvoice;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public IFluentInvoice Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentInvoice;
        }
        //public IFluentInvoice CreateInvoice(InvoiceView invoiceView)
        //{

        //    Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.invoiceRepository.CreateInvoiceByView(invoiceView));
        //    Task.WaitAll(resultTask);
        //    processStatus = resultTask.Result;
        //    return this as IFluentInvoice;

        //}
        public IFluentInvoice MergeWithInvoiceNumber(ref InvoiceView invoiceView)
        {

            string invoiceDocument = invoiceView.InvoiceDocument;

            Task<Invoice> viewTask = Task.Run(() => unitOfWork.invoiceRepository.GetEntityByInvoiceDocument(invoiceDocument));
            Task.WaitAll(viewTask);

            //TODO applicationFactory needs to have a merge feature created
            invoiceView.InvoiceId = viewTask.Result.InvoiceId;
            invoiceView.InvoiceDocument = viewTask.Result.InvoiceDocument;
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
            invoiceView.InvoiceNumber = viewTask.Result.InvoiceNumber;

            return this as IFluentInvoice;
        }

        public IFluentInvoice AddInvoice(List<Invoice> newObjects)
        {
            unitOfWork.invoiceRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentInvoice;
        }
        public IFluentInvoice UpdateInvoice(List<Invoice> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.invoiceRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentInvoice;
        }
        public IFluentInvoice AddInvoice(Invoice newObject)
        {
            unitOfWork.invoiceRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentInvoice;
        }
        public IFluentInvoice UpdateInvoice(Invoice updateObject)
        {
            unitOfWork.invoiceRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentInvoice;

        }
        public IFluentInvoice DeleteInvoice(Invoice deleteObject)
        {
            unitOfWork.invoiceRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentInvoice;
        }
        public IFluentInvoice DeleteInvoice(List<Invoice> deleteObjects)
        {
            unitOfWork.invoiceRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentInvoice;
        }

    }
}
