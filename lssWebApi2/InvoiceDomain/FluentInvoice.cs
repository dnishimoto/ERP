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

namespace lssWebApi2.InvoiceDomain
{
 
 
    public class FluentInvoice : AbstractErrorHandling, IFluentInvoice
    {
        public UnitOfWork unitOfWork;
        public CreateProcessStatus processStatus;

        private FluentInvoiceQuery _query = null;

        public FluentInvoice(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }

        public IFluentInvoiceQuery Query()
        {
            if (_query == null) { _query = new FluentInvoiceQuery(unitOfWork); }

            return _query as IFluentInvoiceQuery;

        }
      
       
        public IFluentInvoice Apply()
        {
            try
            {
                if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
                { unitOfWork.CommitChanges(); }
                return this as IFluentInvoice;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
     

        public IFluentInvoice AddInvoice(List<Invoice> newObjects)
        {
            unitOfWork.invoiceRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentInvoice;
        }
        public IFluentInvoice UpdateInvoice(IList<Invoice> newObjects)
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
