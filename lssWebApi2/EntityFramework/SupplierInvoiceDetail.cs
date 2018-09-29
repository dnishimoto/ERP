using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class SupplierInvoiceDetail
    {
        public long SupplierInvoiceDetailId { get; set; }
        public long SupplierInvoiceId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? ExtendedCost { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }

        public ItemMaster Item { get; set; }
        public SupplierInvoice SupplierInvoice { get; set; }
    }
}
