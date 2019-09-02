using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using lssWebApi2.Enumerations;
using System;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentsDetailView
    {
        public long ShipmentDetailId { get; set; }
        public long ShipmentId { get; set; }
        public long ShipmentDetailNumber { get; set; }
        public long ItemId { get; set; }
        public long? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public long SalesOrderDetailId { get; set; }
        public long? QuantityShipped { get; set; }
        public decimal? AmountShipped { get; set; }
        public string Note { get; set; }

        public decimal? UnitPrice { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnitOfMeasure { get; set; }

        public decimal? Volume { get; set; }
        public string VolumeUnitOfMeasure { get; set; }
        public DateTime? ShippedDate { get; set; }
    }

    public class ShipmentsDetailRepository : Repository<ShipmentsDetail>, IShipmentsDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public ShipmentsDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        private IQueryable<SalesOrderDetail> GetSalesOrderDetailBySalesOrderId(long salesOrderId)
        {
            IQueryable<SalesOrderDetail> query = _dbContext.SalesOrderDetail.Where(e => e.SalesOrderId == salesOrderId).Select(e => e);
            return query;
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await base.GetNextNumber(TypeOfNextNumberEnum.ShipmentsDetailNumber.ToString());
        }
        public async Task<List<ShipmentsDetail>> CreateShipmentsDetailBySalesOrder(ShipmentCreationView shipmentCreation)
        {
            try
            {
                var query = GetSalesOrderDetailBySalesOrderId(shipmentCreation.SalesOrderId);



                List<ShipmentsDetail> list = new List<ShipmentsDetail>();
                foreach (var item in query)
                {
                    ItemMaster itemMaster = await _dbContext.ItemMaster.FindAsync(item.ItemId);

                    NextNumber nnShipmentsDetail = await GetNextNumber();
                    ShipmentsDetail shipmentsDetail = new ShipmentsDetail()
                    {
                        ShipmentDetailNumber = nnShipmentsDetail.NextNumberValue,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity??0,
                        Amount = item.Amount,
                        SalesOrderDetailId = item.SalesOrderDetailId,
                        QuantityShipped = item.QuantityShipped??0,
                        AmountShipped = item.Amount,
                        ShippedDate = shipmentCreation.ShipmentDate,
                        Note = "",
                        UnitPrice = itemMaster.UnitPrice,
                        Weight = itemMaster.Weight??0,
                        WeightUnitOfMeasure = itemMaster.WeightUnitOfMeasure,
                        Volume = itemMaster.Volume??0,
                        VolumeUnitOfMeasure = itemMaster.VolumeUnitOfMeasure

                    };
                    ItemsAdjustedQuantityShippedStruct shipmentAdjustment = shipmentCreation.ItemsAdjustedQuantityShipped.Where(e => e.SalesOrderDetailId == item.SalesOrderDetailId).FirstOrDefault();
                    if (shipmentAdjustment.AdjustedQuantityShipped != 0)
                    {
                        shipmentsDetail.QuantityShipped = shipmentAdjustment.AdjustedQuantityShipped;
                        shipmentsDetail.AmountShipped = shipmentAdjustment.AdjustedAmountShipped;
                    }
                    list.Add(shipmentsDetail);
                }
                await Task.Yield();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<ShipmentsDetail> GetEntityById(long shipmentDetailId)
        {
            return await _dbContext.FindAsync<ShipmentsDetail>(shipmentDetailId);
        }

        public async Task<ShipmentsDetail> GetEntityByNumber(long shipmentDetailNumber)
        {
            var query = await (from detail in _dbContext.ShipmentsDetail
                               where detail.ShipmentDetailNumber == shipmentDetailNumber
                               select detail).FirstOrDefaultAsync<ShipmentsDetail>();
            return query;
        }
        public async Task<List<ShipmentsDetail>> GetEntitiesByShipmentId(long shipmentId)
        {
            var query =
                await (from detail in _dbContext.ShipmentsDetail
                       where detail.ShipmentId == shipmentId
                       select detail).ToListAsync<ShipmentsDetail>();
            return query;
        }
    }
}
