using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.SupplierInvoiceDetailDomain;
using lssWebApi2.Enumerations;
using lssWebApi2.SupplierInvoiceDomain;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{

public class FluentSupplierInvoiceDetail :IFluentSupplierInvoiceDetail
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentSupplierInvoiceDetail(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentSupplierInvoiceDetailQuery Query()
        {
            return new FluentSupplierInvoiceDetailQuery(unitOfWork) as IFluentSupplierInvoiceDetailQuery;
        }
        private SupplierInvoiceDetail MapToEntity(SupplierInvoiceDetailView inputObject)
        {
            Mapper mapper = new Mapper();
            SupplierInvoiceDetail outObject = mapper.Map<SupplierInvoiceDetail>(inputObject);
            return outObject;
        }
        public IFluentSupplierInvoiceDetail CreateSupplierInvoiceDetailsByView(SupplierInvoiceView view)
        {

            Task<SupplierInvoice> supplierInvoiceTask = Task.Run(async () => await unitOfWork.supplierInvoiceRepository.GetEntityByNumber(view.SupplierInvoiceNumber));
            Task.WaitAll(supplierInvoiceTask);

          
            if (supplierInvoiceTask.Result != null)
            {
                long supplierInvoiceId = supplierInvoiceTask.Result.SupplierInvoiceId;

                List<SupplierInvoiceDetail> list = new List<SupplierInvoiceDetail>();

                foreach (var detailView in view.SupplierInvoiceDetailViews)
                {
                    detailView.SupplierInvoiceId = supplierInvoiceId;

                    SupplierInvoiceDetail newDetail = MapToEntity(detailView);

                    list.Add(newDetail);
       
                }
                AddSupplierInvoiceDetails(list);
                processStatus= CreateProcessStatus.Insert;
                return this as IFluentSupplierInvoiceDetail;
            }
            processStatus=CreateProcessStatus.Failed;
            return this as IFluentSupplierInvoiceDetail;
            }
            public IFluentSupplierInvoiceDetail Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentSupplierInvoiceDetail;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentSupplierInvoiceDetail AddSupplierInvoiceDetails(List<SupplierInvoiceDetail> newObjects)
        {
            unitOfWork.supplierInvoiceDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplierInvoiceDetail;
        }
        public IFluentSupplierInvoiceDetail UpdateSupplierInvoiceDetails(IList<SupplierInvoiceDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.supplierInvoiceDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplierInvoiceDetail;
        }
        public IFluentSupplierInvoiceDetail AddSupplierInvoiceDetail(SupplierInvoiceDetail newObject) {
            unitOfWork.supplierInvoiceDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplierInvoiceDetail;
        }
        public IFluentSupplierInvoiceDetail UpdateSupplierInvoiceDetail(SupplierInvoiceDetail updateObject) {
            unitOfWork.supplierInvoiceDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplierInvoiceDetail;

        }
        public IFluentSupplierInvoiceDetail DeleteSupplierInvoiceDetail(SupplierInvoiceDetail deleteObject) {
            unitOfWork.supplierInvoiceDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplierInvoiceDetail;
        }
   	public IFluentSupplierInvoiceDetail DeleteSupplierInvoiceDetails(List<SupplierInvoiceDetail> deleteObjects)
        {
            unitOfWork.supplierInvoiceDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplierInvoiceDetail;
        }
    }
}
