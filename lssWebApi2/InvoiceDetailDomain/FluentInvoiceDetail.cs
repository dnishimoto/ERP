using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.InvoiceDetailDomain;
using lssWebApi2.Enumerations;
using lssWebApi2.InvoicesDomain;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.InvoiceDetailDomain
{

public class FluentInvoiceDetail :IFluentInvoiceDetail
    {
 private UnitOfWork unitOfWork ;
        private CreateProcessStatus processStatus;

        public FluentInvoiceDetail(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentInvoiceDetailQuery Query()
        {
            return new FluentInvoiceDetailQuery(unitOfWork) as IFluentInvoiceDetailQuery;
        }
        private InvoiceDetail MapToEntity(InvoiceDetailView inputObject)
        {
            Mapper mapper = new Mapper();
            InvoiceDetail outObject = mapper.Map<InvoiceDetail>(inputObject);
            return outObject;
        }
        public IFluentInvoiceDetail Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentInvoiceDetail;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentInvoiceDetail AddInvoiceDetails(List<InvoiceDetail> newObjects)
        {
            unitOfWork.invoiceDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentInvoiceDetail;
        }
        public IFluentInvoiceDetail UpdateInvoiceDetails(IList<InvoiceDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.invoiceDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentInvoiceDetail;
        }
        public IFluentInvoiceDetail AddInvoiceDetail(InvoiceDetail newObject) {
            unitOfWork.invoiceDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentInvoiceDetail;
        }
        public IFluentInvoiceDetail UpdateInvoiceDetail(InvoiceDetail updateObject) {
            unitOfWork.invoiceDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentInvoiceDetail;

        }
        public IFluentInvoiceDetail DeleteInvoiceDetail(InvoiceDetail deleteObject) {
            unitOfWork.invoiceDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentInvoiceDetail;
        }
   	public IFluentInvoiceDetail DeleteInvoiceDetails(List<InvoiceDetail> deleteObjects)
        {
            unitOfWork.invoiceDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentInvoiceDetail;
        }
    }
}
