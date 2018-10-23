using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PurchaseOrderDetail
    {
        public long PurchaseOrderDetailId { get; set; }
        public long PurchaseOrderId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? OrderedQuantity { get; set; }
        public long ItemId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ReceivedQuantity { get; set; }
        public int? RemainingQuantity { get; set; }
        public string Description { get; set; }

        public virtual ItemMaster Item { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

    }
}
