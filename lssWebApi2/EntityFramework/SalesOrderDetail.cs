using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class SalesOrderDetail
    {
        public long SalesOrderDetailId { get; set; }
        public long SalesOrderId { get; set; }
        public long SalesOrderDetailNumber { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public long? Quantity { get; set; }
        public long? QuantityOpen { get; set; }
        public long? QuantityShipped { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountOpen { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitPrice { get; set; }
        public long? InvoiceDetailId { get; set; }
        public long? InvoiceId { get; set; }
        public long? ShipmentId { get; set; }
        public long? ShipmentDetailId { get; set; }
        public long? AccountId { get; set; }
        public long? BuyerId { get; set; }
        public long? CarrierId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? PurchaseOrderDetailId { get; set; }
        public long? PickListId { get; set; }
        public long? PickListDetailId { get; set; }
        public DateTime? ScheduledShipDate { get; set; }
        public DateTime? PromisedDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? GLDate { get; set; }
        public decimal? GrossWeight { get; set; }
        public string GrossWeightUnitOfMeasure { get; set; }
        public decimal? UnitVolume { get; set; }
        public string UnitVolumeUnitOfMeasurement { get; set; }
        public string LotSerial { get; set; }
        public string Location { get; set; }
        public string BusUnit { get; set; }
        public string CompanyNumber { get; set; }
        public long? LineNumber { get; set; }
        public long? ShippedToAddressId { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentInstrument { get; set; }
 
        public virtual ItemMaster Item { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }

    }
}
