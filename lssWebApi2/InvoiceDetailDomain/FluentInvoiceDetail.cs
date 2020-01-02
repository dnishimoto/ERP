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
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentInvoiceDetail() { }
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
        public IFluentInvoiceDetail CreateInvoiceDetailsByInvoiceView(InvoiceView invoiceView)
        {
            try
            {
                int count = 0;

                Task<Invoice> invoiceTask = Task.Run(async () => await unitOfWork.invoiceRepository.GetEntityByInvoiceDocument(invoiceView.InvoiceDocument));
                Task.WaitAll(invoiceTask);

               


                //Assign the InvoiceId
                for (int i = 0; i < invoiceView.InvoiceDetailViews.Count; i++)
                {
                    invoiceView.InvoiceDetailViews[i].InvoiceId = invoiceTask.Result.InvoiceId;
                    InvoiceDetail invoiceDetail =  MapToEntity(invoiceView.InvoiceDetailViews[i]);

                  
                     AddInvoiceDetail(invoiceDetail);
                    if (processStatus == CreateProcessStatus.Insert) { count++; }
                }
                if (count == 0) {  processStatus=CreateProcessStatus.AlreadyExists;   }
                return this as IFluentInvoiceDetail;
            }
            catch (Exception ex)
            { throw new Exception("CreateInvoiceDetailsByView", ex); }

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
        public IFluentInvoiceDetail UpdateInvoiceDetails(List<InvoiceDetail> newObjects)
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
