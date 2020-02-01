using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.Enumerations;
using lssWebApi2.PurchaseOrderDomain;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.PurchaseOrderDetailDomain
{

public class FluentPurchaseOrderDetail :IFluentPurchaseOrderDetail
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentPurchaseOrderDetail(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPurchaseOrderDetailQuery Query()
        {
            return new FluentPurchaseOrderDetailQuery(unitOfWork) as IFluentPurchaseOrderDetailQuery;
        }
        public IFluentPurchaseOrderDetail Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPurchaseOrderDetail;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        private PurchaseOrderDetail MapToEntity(PurchaseOrderDetailView inputObject)
        {
            Mapper mapper = new Mapper();
            PurchaseOrderDetail outObject = mapper.Map<PurchaseOrderDetail>(inputObject);
            return outObject;
        }
        public IFluentPurchaseOrderDetail CreatePurchaseOrderDetailsByView(PurchaseOrderView purchaseOrderView)
        {
            try
            {
                Task<PurchaseOrder> purchaseOrderTask = Task.Run(async () => await unitOfWork.purchaseOrderRepository.GetEntityByOrderNumber(purchaseOrderView.Ponumber));
                Task.WaitAll(purchaseOrderTask);


                if (purchaseOrderTask.Result != null)
                {
                    long purchaseOrderId = purchaseOrderTask.Result.PurchaseOrderId;

                    foreach (var detailView in purchaseOrderView.PurchaseOrderDetailViews)
                    {
                        detailView.PurchaseOrderId = purchaseOrderId;

                        PurchaseOrderDetail poDetail = MapToEntity(detailView);

                        Task<PurchaseOrderDetail> detailLookupTask =
                            Task.Run(async () => await unitOfWork.purchaseOrderDetailRepository.FindEntityByExpression
                            (e =>  e.PurchaseOrderId == purchaseOrderId
                        ));
                        Task.WaitAll(detailLookupTask);

                     
                        if (detailLookupTask.Result == null)
                        {
                            AddPurchaseOrderDetail(poDetail);

                        }
                    }

                }
                return this as IFluentPurchaseOrderDetail;
            }
            catch (Exception ex) { throw new Exception("CreatePurchaseOrderDetailsByView", ex); }
        }
        public IFluentPurchaseOrderDetail AddPurchaseOrderDetails(List<PurchaseOrderDetail> newObjects)
        {
            unitOfWork.purchaseOrderDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPurchaseOrderDetail;
        }
        public IFluentPurchaseOrderDetail UpdatePurchaseOrderDetails(IList<PurchaseOrderDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.purchaseOrderDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPurchaseOrderDetail;
        }
        public IFluentPurchaseOrderDetail AddPurchaseOrderDetail(PurchaseOrderDetail newObject) {
            unitOfWork.purchaseOrderDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPurchaseOrderDetail;
        }
        public IFluentPurchaseOrderDetail UpdatePurchaseOrderDetail(PurchaseOrderDetail updateObject) {
            unitOfWork.purchaseOrderDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPurchaseOrderDetail;

        }
        public IFluentPurchaseOrderDetail DeletePurchaseOrderDetail(PurchaseOrderDetail deleteObject) {
            unitOfWork.purchaseOrderDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPurchaseOrderDetail;
        }
   	public IFluentPurchaseOrderDetail DeletePurchaseOrderDetails(List<PurchaseOrderDetail> deleteObjects)
        {
            unitOfWork.purchaseOrderDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPurchaseOrderDetail;
        }
    }
}
