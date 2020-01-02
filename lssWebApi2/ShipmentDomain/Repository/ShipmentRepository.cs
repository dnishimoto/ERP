
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace lssWebApi2.ShipmentsDomain
{
    public struct ItemsAdjustedQuantityShippedStruct
    {
        public long SalesOrderDetailId;
        public long AdjustedQuantityShipped;
        public decimal AdjustedAmountShipped;
    }
    public class ShipmentView
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
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public string WeightUOM { get; set; }
        public long? SalesOrderId { get; set; }
        public List<ItemsAdjustedQuantityShippedStruct> ItemsAdjustedQuantityShipped { get; set; }
        public IList<ShipmentDetailView> ShipmentDetailViews { get; set; }
    }
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        ListensoftwaredbContext _dbContext;
        public ShipmentRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }



        public  IQueryable<Shipment> GetEntitiesByExpression(Expression<Func<Shipment, bool>> predicate)
        {
            var result = _dbContext.Set<Shipment>().Where(predicate);
    
            return result;
        }


        public async Task<NextNumber> GetNextNumber()
        {
            return await base.GetNextNumber(TypeOfShipment.ShipmentNumber.ToString());
        }
     
        private async Task<SalesOrder> GetSalesOrderById(long salesOrderId)
        {
            return await _dbContext.SalesOrder.FindAsync(salesOrderId);
        }
            public async Task<Shipment> GetEntityById(long ? shipmentsId)
        {
            return await _dbContext.FindAsync<Shipment>(shipmentsId);
        }

        public async Task<Shipment> GetEntityByNumber(long shipmentsNumber)
        {
            var query = await (from detail in _dbContext.Shipment
                               where detail.ShipmentNumber == shipmentsNumber
                               select detail).FirstOrDefaultAsync<Shipment>();
            return query;
        }
    }
}
