using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.ShipmentsDomain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentsDetailView
    {
        public long ShipmentDetailId { get; set; }
        public long ShipmentDetailNumber { get; set; }
        public long ShipmentId { get; set; }
        public long ItemId { get; set; }
        public long? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public long SalesOrderDetailId { get; set; }
        public long? QuantityOpen { get; set; }
        public decimal? AmountOpen { get; set; }
        public string Note { get; set; }
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
            var query = GetSalesOrderDetailBySalesOrderId(shipmentCreation.SalesOrderId);

            List<ShipmentsDetail> list = new List<ShipmentsDetail>();
            foreach (var item in query)
            {
                NextNumber nnShipmentsDetail = await GetNextNumber();
                ShipmentsDetail shipmentsDetail = new ShipmentsDetail()
                {
                    ShipmentDetailNumber = nnShipmentsDetail.NextNumberValue,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    SalesOrderDetailId = item.SalesOrderDetailId,
                    QuantityShipped = item.QuantityShipped,
                    AmountShipped = item.Amount,
                    Note = ""
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
