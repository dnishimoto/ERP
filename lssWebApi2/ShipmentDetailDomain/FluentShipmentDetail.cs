using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{

    public class FluentShipmentDetail : IFluentShipmentDetail
    {
        private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentShipmentDetail(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentShipmentDetailQuery Query()
        {
            return new FluentShipmentDetailQuery(unitOfWork) as IFluentShipmentDetailQuery;
        }
        public IFluentShipmentDetail CreateShipmentsDetailBySalesOrderView(ShipmentView shipmentCreation)
        {
            try
            {
                var query = unitOfWork.salesOrderDetailRepository.GetIQueryableEntitiesBySalesOrderId(shipmentCreation.SalesOrderId??0);



                List<ShipmentDetail> list = new List<ShipmentDetail>();
                foreach (var item in query)
                {
                    Task<ItemMaster> itemMasterTask = Task.Run(async()=>await unitOfWork.itemMasterRepository.GetEntityById(item.ItemId));
                    Task<NextNumber> nnShipmentDetailTask = Task.Run(async () => await unitOfWork.nextNumberRepository.GetNextNumber(TypeOfShipmentDetail.ShipmentsDetailNumber.ToString()));
                    Task.WaitAll(itemMasterTask, nnShipmentDetailTask);

                    ShipmentDetail shipmentsDetail = new ShipmentDetail()
                    {
                        ShipmentDetailNumber = nnShipmentDetailTask.Result.NextNumberValue,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity ?? 0,
                        Amount = item.Amount,
                        SalesOrderDetailId = item.SalesOrderDetailId,
                        QuantityShipped = item.QuantityShipped ?? 0,
                        AmountShipped = item.Amount,
                        ShippedDate = shipmentCreation.ShipmentDate,
                        Note = "",
                        UnitPrice = itemMasterTask.Result.UnitPrice,
                        Weight = itemMasterTask.Result.Weight ?? 0,
                        WeightUnitOfMeasure = itemMasterTask.Result.WeightUnitOfMeasure,
                        Volume = itemMasterTask.Result.Volume ?? 0,
                        VolumeUnitOfMeasure = itemMasterTask.Result.VolumeUnitOfMeasure

                    };
                    ItemsAdjustedQuantityShippedStruct shipmentAdjustment = shipmentCreation.ItemsAdjustedQuantityShipped.Where(e => e.SalesOrderDetailId == item.SalesOrderDetailId).FirstOrDefault();
                    if (shipmentAdjustment.AdjustedQuantityShipped != 0)
                    {
                        shipmentsDetail.QuantityShipped = shipmentAdjustment.AdjustedQuantityShipped;
                        shipmentsDetail.AmountShipped = shipmentAdjustment.AdjustedAmountShipped;
                    }
                    list.Add(shipmentsDetail);
                }
                AddShipmentDetails(list);
                return this as IFluentShipmentDetail;
            }
            catch (Exception ex)
            {
                throw new Exception("CreateShipmentsDetailBySalesOrderView", ex);
            }
        }
        public IFluentShipmentDetail Apply()
        {
            try
            {
                if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
                { unitOfWork.CommitChanges(); }
                return this as IFluentShipmentDetail;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }
        public IFluentShipmentDetail AddShipmentDetails(List<ShipmentDetail> newObjects)
        {
            unitOfWork.shipmentDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipmentDetail;
        }
        public IFluentShipmentDetail UpdateShipmentDetails(IList<ShipmentDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.shipmentDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipmentDetail;
        }
        public IFluentShipmentDetail AddShipmentDetail(ShipmentDetail newObject)
        {
            unitOfWork.shipmentDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentShipmentDetail;
        }
        public IFluentShipmentDetail UpdateShipmentDetail(ShipmentDetail updateObject)
        {
            unitOfWork.shipmentDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentShipmentDetail;

        }
        public IFluentShipmentDetail DeleteShipmentDetail(ShipmentDetail deleteObject)
        {
            unitOfWork.shipmentDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipmentDetail;
        }
        public IFluentShipmentDetail DeleteShipmentDetails(List<ShipmentDetail> deleteObjects)
        {
            unitOfWork.shipmentDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentShipmentDetail;
        }
    }
}