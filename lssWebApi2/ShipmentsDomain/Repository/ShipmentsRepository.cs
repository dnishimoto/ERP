
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentsView
    {
        public long ShipmentId { get; set; }
        public long ShipmentNumber { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public long CustomerId { get; set; }
        public long CarrierId { get; set; }
        public string TrackingNumber { get; set; }
        public decimal? ActualWeight { get; set; }
        public decimal? BillableWeight { get; set; }
        public decimal? Duty { get; set; }
        public decimal? Tax { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CodAmount { get; set; }
        public long ShippedFromLocationId { get; set; }
        public long? ShippedToLocationId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? VendorInvoiceId { get; set; }
        public decimal? VendorShippingCost { get; set; }
        public decimal? VendorHandlingCost { get; set; }
    }
    public class ShipmentsRepository : Repository<Shipments>, IShipmentsRepository
    {
        ListensoftwaredbContext _dbContext;
        public ShipmentsRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }


        public async Task<Shipments> GetEntityById(long shipmentsId)
        {
            return await _dbContext.FindAsync<Shipments>(shipmentsId);
        }

        public async Task<Shipments> GetEntityByNumber(long shipmentsNumber)
        {
            var query = await (from detail in _dbContext.Shipments
                               where detail.ShipmentNumber == shipmentsNumber
                               select detail).FirstOrDefaultAsync<Shipments>();
            return query;
        }
    }
}
