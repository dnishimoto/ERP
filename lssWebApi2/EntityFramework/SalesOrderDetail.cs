using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class SalesOrderDetail
    {
        public long SalesOrderDetailId { get; set; }
        public long SalesOrderId { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? UnitPrice { get; set; }
        public long? InvoiceDetailId { get; set; }

        public virtual ItemMaster Item { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
    }
}
