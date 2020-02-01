using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{

    public class FluentShipmentDetailQuery : MapperAbstract<ShipmentDetail, ShipmentDetailView>, IFluentShipmentDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentShipmentDetailQuery() { }
        public FluentShipmentDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public override async Task<ShipmentDetail> MapToEntity(ShipmentDetailView inputObject)
        {

            ShipmentDetail outObject = mapper.Map<ShipmentDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<ShipmentDetail>> MapToEntity(IList<ShipmentDetailView> inputObjects)
        {
            IList<ShipmentDetail> list = new List<ShipmentDetail>();

            foreach (var item in inputObjects)
            {
                ShipmentDetail outObject = mapper.Map<ShipmentDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public override async Task<ShipmentDetailView> MapToView(ShipmentDetail inputObject)
        {

            ShipmentDetailView outObject = mapper.Map<ShipmentDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<IList<ShipmentDetail>> GetEntitiesByShipmentId(long ? shipmentId)
        {
            return await _unitOfWork.shipmentDetailRepository.GetEntitiesByShipmentId(shipmentId);
        }
        public async Task<List<ShipmentDetail>> GetShipmentDetailBySalesOrder(ShipmentView shipmentView)
        {
                try
                {

                    IList<SalesOrderDetail> query =await _unitOfWork.salesOrderDetailRepository.GetEntitiesBySalesOrderId(shipmentView.SalesOrderId ?? 0);


                    List<ShipmentDetail> list = new List<ShipmentDetail>();

                    foreach (var item in query)

                    {

                        ItemMaster itemMaster =  await _unitOfWork.itemMasterRepository.GetEntityById(item.ItemId);
                        NextNumber nnShipmentDetail =  await  _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfShipmentDetail.ShipmentsDetailNumber.ToString());
 
                        ShipmentDetail shipmentDetail = new ShipmentDetail()

                        {
                            ShipmentDetailNumber = nnShipmentDetail.NextNumberValue,
                            ItemId = item.ItemId,
                            Quantity = item.Quantity ?? 0,
                            Amount = item.Amount,
                            SalesOrderDetailId = item.SalesOrderDetailId,
                            QuantityShipped = item.QuantityShipped ?? 0,
                            AmountShipped = item.Amount,
                            ShippedDate = shipmentView.ShipmentDate,
                            Note = "",
                            UnitPrice = itemMaster.UnitPrice,
                            Weight = itemMaster.Weight ?? 0,
                            WeightUnitOfMeasure = itemMaster.WeightUnitOfMeasure,
                            Volume = itemMaster.Volume ?? 0,
                            VolumeUnitOfMeasure = itemMaster.VolumeUnitOfMeasure
                        };

                        ItemsAdjustedQuantityShippedStruct shipmentAdjustment = shipmentView.ItemsAdjustedQuantityShipped.Where(e => e.SalesOrderDetailId == item.SalesOrderDetailId).FirstOrDefault();

                        if (shipmentAdjustment.AdjustedQuantityShipped != 0)

                        {
                            shipmentDetail.QuantityShipped = shipmentAdjustment.AdjustedQuantityShipped;
                            shipmentDetail.AmountShipped = shipmentAdjustment.AdjustedAmountShipped;
                        }

                        list.Add(shipmentDetail);

                    }

                    await Task.Yield();

                    return list;

                }

                catch (Exception ex)

                {

                    throw new Exception("GetShipmentDetailBySalesOrder", ex);

                }

            }

        public async Task<IList<ShipmentDetailView>> GetViewsByShipmentId(long ? shipmentId)
        {
            IList<ShipmentDetailView> listViews = new List<ShipmentDetailView>();
            IList<ShipmentDetail> list = await _unitOfWork.shipmentDetailRepository.GetEntitiesByShipmentId(shipmentId);
            foreach (var item in list)
            {
                listViews.Add(await MapToView(item));
            }

            return listViews;
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfShipmentDetail.ShipmentsDetailNumber.ToString());
        }
        public override async Task<ShipmentDetailView> GetViewById(long ? shipmentDetailId)
        {
            ShipmentDetail detailItem = await _unitOfWork.shipmentDetailRepository.GetEntityById(shipmentDetailId);

            return await MapToView(detailItem);
        }
        public async Task<ShipmentDetailView> GetViewByNumber(long shipmentDetailNumber)
        {
            ShipmentDetail detailItem = await _unitOfWork.shipmentDetailRepository.GetEntityByNumber(shipmentDetailNumber);

            return await MapToView(detailItem);
        }
        public override async Task<ShipmentDetail> GetEntityById(long ? shipmentDetailId) {
            return await _unitOfWork.shipmentDetailRepository.GetEntityById(shipmentDetailId);
        }
        public async Task<ShipmentDetail> GetEntityByNumber(long shipmentDetailNumber) {
            return await _unitOfWork.shipmentDetailRepository.GetEntityByNumber(shipmentDetailNumber);
        }

    }
}