using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.Enumerations;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;

namespace lssWebApi2.PurchaseOrderDomain
{

public class FluentPurchaseOrder :IFluentPurchaseOrder
    {
 private UnitOfWork unitOfWork ;
        private CreateProcessStatus processStatus;

        public FluentPurchaseOrder(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPurchaseOrderQuery Query()
        {
            return new FluentPurchaseOrderQuery(unitOfWork) as IFluentPurchaseOrderQuery;
        }
        private PurchaseOrder MapToEntity(PurchaseOrderView inputObject)
        {
            Mapper mapper = new Mapper();
            PurchaseOrder outObject = mapper.Map<PurchaseOrder>(inputObject);
            return outObject;
        }
        public IFluentPurchaseOrder CreatePurchaseOrderByView(PurchaseOrderView purchaseOrderView)
        {
            decimal amount = 0;
            try

            {
                //check if PO exists
                Task<PurchaseOrder> purchaseOrderTask = Task.Run(async () => await unitOfWork.purchaseOrderRepository.GetEntityByOrderNumber(purchaseOrderView.Ponumber));
                Task.WaitAll(purchaseOrderTask);

                if (purchaseOrderTask.Result != null) { processStatus= CreateProcessStatus.AlreadyExists; return this as IFluentPurchaseOrder; }


                foreach (var detail in purchaseOrderView.PurchaseOrderDetailViews)
                {
                    amount += detail.Amount ?? 0;
                }
                purchaseOrderView.Amount = amount;
                purchaseOrderView.AmountPaid = 0;

                Task<TaxRatesByCode> taxTask = unitOfWork.taxRateByCodeRepository.GetEntityByCode(purchaseOrderView.TaxCode1);
                Task.WaitAll(taxTask);
                purchaseOrderView.Tax = amount * taxTask.Result.TaxRate;

                PurchaseOrder po = MapToEntity(purchaseOrderView);

                AddPurchaseOrder(po);

                return this as IFluentPurchaseOrder;
            }
            catch (Exception ex) { throw new Exception("CreatePurchaseOrderByView", ex); }
        }

        public IFluentPurchaseOrder Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPurchaseOrder;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPurchaseOrder AddPurchaseOrders(List<PurchaseOrder> newObjects)
        {
            unitOfWork.purchaseOrderRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPurchaseOrder;
        }
        public IFluentPurchaseOrder UpdatePurchaseOrders(IList<PurchaseOrder> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.purchaseOrderRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPurchaseOrder;
        }
        public IFluentPurchaseOrder AddPurchaseOrder(PurchaseOrder newObject) {
            unitOfWork.purchaseOrderRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPurchaseOrder;
        }
        public IFluentPurchaseOrder UpdatePurchaseOrder(PurchaseOrder updateObject) {
            unitOfWork.purchaseOrderRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPurchaseOrder;

        }
        public IFluentPurchaseOrder DeletePurchaseOrder(PurchaseOrder deleteObject) {
            unitOfWork.purchaseOrderRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPurchaseOrder;
        }
   	public IFluentPurchaseOrder DeletePurchaseOrders(List<PurchaseOrder> deleteObjects)
        {
            unitOfWork.purchaseOrderRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPurchaseOrder;
        }
    }
}
