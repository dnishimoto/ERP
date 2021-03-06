namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InvoiceDetail")]
    public partial class InvoiceDetail
    {
        public long InvoiceDetailId { get; set; }

        public long InvoiceId { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure { get; set; }

        public decimal? Amount { get; set; }

        public long? PurchaseOrderLineId { get; set; }

        public long? SalesOrderDetailId { get; set; }

        public long ItemId { get; set; }

        public decimal? DiscountPercent { get; set; }

        public decimal? DiscountAmount { get; set; }

        public long? ShipmentDetailId { get; set; }

        [StringLength(255)]
        public string ExtendedDescription { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DiscountDueDate { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }
    }
}
