namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseOrderDetail")]
    public partial class PurchaseOrderDetail
    {
        public long PurchaseOrderDetailId { get; set; }

        public long PurchaseOrderId { get; set; }

        public decimal? Amount { get; set; }

        public decimal? OrderedQuantity { get; set; }

        public long ItemId { get; set; }

        public decimal? UnitPrice { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceivedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpectedDeliveryDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OrderDate { get; set; }

        public int? ReceivedQuantity { get; set; }

        public int? RemainingQuantity { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
