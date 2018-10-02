using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class SalesOrder
    {
        public SalesOrder()
        {
            SalesOrderDetail = new HashSet<SalesOrderDetail>();
        }

        public long SalesOrderId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public long CustomerId { get; set; }
        public long? DeliveredToLocationId { get; set; }
        public long? ShippedToLocationId { get; set; }
        public long? InvoiceId { get; set; }
        public string TakenBy { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? FreightAmount { get; set; }
        public long? CarrierId { get; set; }
        public long? BuyerId { get; set; }
        public string PaymentInstrument { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? ScheduledPickupDate { get; set; }
        public DateTime? ActualPickupDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<SalesOrderDetail> SalesOrderDetail { get; set; }
    }
}
