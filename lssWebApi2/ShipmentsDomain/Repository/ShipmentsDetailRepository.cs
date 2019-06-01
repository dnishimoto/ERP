using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.ShipmentsDomain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentsDetailView
    {
        public long ShipmentDetailId { get; set; }
        public long ShipmentDetailNumber { get; set; }
        public long ShipmentId { get; set; }
        public long ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public long SalesOrderDetailId { get; set; }
    }

    public class ShipmentsDetailRepository : Repository<ShipmentsDetail>, IShipmentsDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public ShipmentsDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
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
