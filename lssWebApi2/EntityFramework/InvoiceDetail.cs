using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class InvoiceDetail
    {
        public long InvoiceDetailId { get; set; }
        public long InvoiceId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? Amount { get; set; }
        public long? PurchaseOrderLineId { get; set; }
        public long? SalesOrderDetailId { get; set; }
        public long ItemId { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public long? ShipmentDetailId { get; set; }
        public string ExtendedDescription { get; set; }
        public DateTime? DiscountDueDate { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual ItemMaster Item { get; set; }
    }
}
