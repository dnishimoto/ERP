using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using lssWebApi2.Enumerations;
using System;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentDetailView
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

    public class ShipmentDetailRepository : Repository<ShipmentDetail>, IShipmentDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public ShipmentDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
      
         
        public async Task<ShipmentDetail> GetEntityById(long ? shipmentDetailId)
        {
            return await _dbContext.FindAsync<ShipmentDetail>(shipmentDetailId);
        }

        public async Task<ShipmentDetail> GetEntityByNumber(long shipmentDetailNumber)
        {
            var query = await (from detail in _dbContext.ShipmentDetail
                               where detail.ShipmentDetailNumber == shipmentDetailNumber
                               select detail).FirstOrDefaultAsync<ShipmentDetail>();
            return query;
        }
        public async Task<IList<ShipmentDetail>> GetEntitiesByShipmentId(long ? shipmentId)
        {
            var query =
                await (from detail in _dbContext.ShipmentDetail
                       where detail.ShipmentId == shipmentId
                       select detail).ToListAsync<ShipmentDetail>();
            return query;
        }
    }
}
