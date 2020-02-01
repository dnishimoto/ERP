using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDetailDomain
{
    public class FluentSalesOrderDetail : IFluentSalesOrderDetail
    {
        private UnitOfWork unitOfWork ;
        private CreateProcessStatus processStatus;

        public FluentSalesOrderDetail(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentSalesOrderDetailQuery Query()
        {
            return new FluentSalesOrderDetailQuery(unitOfWork) as IFluentSalesOrderDetailQuery;
        }
        public IFluentSalesOrderDetail UpdateSalesOrderDetailByShipmentsDetail(IEnumerable<ShipmentDetail> shipmentsDetails)
        {
            try
            {
                foreach (var item in shipmentsDetails)
                {
                    Task<SalesOrderDetail> detailTask = Task.Run(async () => await unitOfWork.salesOrderDetailRepository.GetEntityById(item.SalesOrderDetailId));
                    Task.WaitAll(detailTask);


                    detailTask.Result.ShippedDate = item.ShippedDate;
                    detailTask.Result.QuantityShipped = item.QuantityShipped;
                    detailTask.Result.QuantityOpen = item.Quantity - item.QuantityShipped;
                    detailTask.Result.AmountOpen = item.Amount = item.AmountShipped;

                    UpdateSalesOrderDetail(detailTask.Result);
                }
                return this as IFluentSalesOrderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IFluentSalesOrderDetail Apply()
        {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail AddSalesOrderDetails(List<SalesOrderDetail> newObjects)
        {
            unitOfWork.salesOrderDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail UpdateSalesOrderDetails(IList<SalesOrderDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.salesOrderDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail AddSalesOrderDetail(SalesOrderDetail newObject)
        {
            unitOfWork.salesOrderDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail UpdateSalesOrderDetail(SalesOrderDetail updateObject)
        {
            unitOfWork.salesOrderDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSalesOrderDetail;

        }
        public IFluentSalesOrderDetail DeleteSalesOrderDetails(List<SalesOrderDetail> deleteObjects)
        {
            unitOfWork.salesOrderDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSalesOrderDetail;
        }
        public IFluentSalesOrderDetail DeleteSalesOrderDetail(SalesOrderDetail deleteObject)
        {
            unitOfWork.salesOrderDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSalesOrderDetail;
        }
    }
}
