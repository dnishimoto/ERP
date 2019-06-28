using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentSalesOrder :IFluentSalesOrder
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentSalesOrder() { }
        public IFluentSalesOrderQuery Query()
        {
            return new FluentSalesOrderQuery(unitOfWork) as IFluentSalesOrderQuery;
        }
        public IFluentSalesOrder Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentSalesOrder;
        }
        public IFluentSalesOrder UpdateSalesOrderAmountByShipmentsDetail(Shipments shipments, decimal? amount)
        {
            Task<SalesOrder> salesOrderTask = Task.Run(async () => await unitOfWork.salesOrderRepository.GetEntityById(shipments.SalesOrderId??0));
            Task.WaitAll(salesOrderTask);
            SalesOrder salesOrder = salesOrderTask.Result;
            salesOrder.Amount = amount;
            unitOfWork.salesOrderRepository.UpdateObject(salesOrder);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSalesOrder;
        }
        public IFluentSalesOrder AddSalesOrder(SalesOrder newObject) {
            unitOfWork.salesOrderRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSalesOrder;
        }
        public IFluentSalesOrder UpdateSalesOrder(SalesOrder updateObject) {
            unitOfWork.salesOrderRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSalesOrder;

        }
        public IFluentSalesOrder DeleteSalesOrder(SalesOrder deleteObject) {
            unitOfWork.salesOrderRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSalesOrder;
        }
    }
}
