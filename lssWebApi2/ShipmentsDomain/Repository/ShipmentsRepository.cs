
using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.AbstractFactory;
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
    public class ShipmentCreationView {
        public ShipmentCreationView() { ItemsAdjustedQuantityShipped = new List<ItemsAdjustedQuantityShippedStruct>(); }
        public long SalesOrderId { get; set; }
        public decimal? ActualWeight { get; set; }
        public decimal? BillableWeight { get; set; }
        public decimal Amount { get; set; }
        public decimal CodAmount { get; set; }
        public long ShippedFromLocationId { get; set; }
        public long ShippedToLocationId { get; set; }
        public string TrackingNumber { get; set; }
        public string WeightUOM { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public long CarrierId { get; set; }
        public decimal? Duty { get; set; }
        public decimal? Tax { get; set; }
        public decimal? ShippingCost { get; set; }
        public List<ItemsAdjustedQuantityShippedStruct> ItemsAdjustedQuantityShipped {get;set;} 
        
    }
    public class ShipmentsView
    {

        public ShipmentsView()
        {
            if (ShipmentsDetailViews == null)
            {
                ShipmentsDetailViews = new List<ShipmentsDetailView>();
            }
        }
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
        public List<ShipmentsDetailView> ShipmentsDetailViews { get; set; }
    }
    public class ShipmentsRepository : Repository<Shipments>, IShipmentsRepository
    {
        ListensoftwaredbContext _dbContext;
        public ShipmentsRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<ShipmentsView> MapToView(Shipments inputObject)
        {
            Mapper mapper = new Mapper();
            ShipmentsView outObject = mapper.Map<ShipmentsView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<ShipmentsDetailView> MapToDetailView(ShipmentsDetail inputObject)
        {
            Mapper mapper = new Mapper();
            ShipmentsDetailView outObject = mapper.Map<ShipmentsDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }

      

        public async Task<Shipments> CreateShipmentBySalesOrder(ShipmentCreationView shipmentCreation)
        {

            SalesOrder salesOrder = await GetSalesOrderById(shipmentCreation.SalesOrderId);

            NextNumber nnShipments = await GetNextNumber();
            Shipments shipment = new Shipments()
            {
                CustomerId = salesOrder.CustomerId,
                OrderNumber =salesOrder.OrderNumber,
                OrderType =salesOrder.OrderType,
                Duty = shipmentCreation.Duty,
                Tax = shipmentCreation.Tax,
                ShippingCost = shipmentCreation.ShippingCost,
                Amount = shipmentCreation.Amount,
                CodAmount= shipmentCreation.CodAmount,
                ShipmentNumber = nnShipments.NextNumberValue,
                ActualWeight = shipmentCreation.ActualWeight,
                BillableWeight = shipmentCreation.BillableWeight,
                ShippedFromLocationId = shipmentCreation.ShippedFromLocationId,
                ShippedToLocationId = shipmentCreation.ShippedToLocationId,
                TrackingNumber = shipmentCreation.TrackingNumber,
                WeightUOM = shipmentCreation.WeightUOM,
                ShipmentDate = DateTime.Now,
                CarrierId = shipmentCreation.CarrierId
            };



            return shipment;
           

        }
     
        public async Task<NextNumber> GetNextNumber()
        {
            return await base.GetNextNumber(TypeOfNextNumberEnum.ShipmentsNumber.ToString());
        }
     
        private async Task<SalesOrder> GetSalesOrderById(long salesOrderId)
        {
            return await _dbContext.SalesOrder.FindAsync(salesOrderId);
        }
        //public async Task<PageListViewContainer<ShipmentsView>> GetViewsByPage(System.Linq.Expressions.Expression<System.Func<Shipments, bool>> predicate, System.Linq.Expressions.Expression<System.Func<Shipments, object>> order, int pageSize, int pageNumber)
        public async Task<PageListViewContainer<ShipmentsView>> GetViewsByPage(Func<Shipments, bool> predicate, Func<Shipments, object> order, int pageSize, int pageNumber)
        {
            try
            {
                //IEnumerable<SalesOrder> query = _dbContext.SalesOrder.Where(predicate).OrderByDescending(order).Select(e => e);
                var query = _dbContext.Shipments.Where(predicate).OrderByDescending(order).Select(e => e);

                IPagedList<Shipments> list = await query.ToPagedListAsync(pageNumber, pageSize);

                PageListViewContainer<ShipmentsView> container = new PageListViewContainer<ShipmentsView>();
                container.PageNumber = pageNumber;
                container.PageSize = pageSize;
                container.TotalItemCount = list.TotalItemCount;

                ShipmentsView view;
                foreach (var item in list)
                {
                     view = await MapToView(item);


                    var query2 = from shipmentsDetail in _dbContext.ShipmentsDetail
                                 where shipmentsDetail.ShipmentId == item.ShipmentId
                                 select shipmentsDetail;


                    foreach (var item2 in query2)
                    {
                        view.ShipmentsDetailViews.Add(await MapToDetailView(item2));
                    }


                    container.Items.Add(view);
                }

                //await Task.Yield();
                return container;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

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
