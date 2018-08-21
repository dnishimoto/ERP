namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplierInvoiceDetail")]
    public partial class SupplierInvoiceDetail
    {
        public long SupplierInvoiceDetailId { get; set; }

        public long SupplierInvoiceId { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? Quantity { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure { get; set; }

        public decimal? ExtendedCost { get; set; }

        public long ItemId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DiscountDueDate { get; set; }

        public decimal? DiscountAmount { get; set; }

        public decimal? DiscountPercent { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }

        public virtual SupplierInvoice SupplierInvoice { get; set; }
    }
}
