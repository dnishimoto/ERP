namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplierInvoice")]
    public partial class SupplierInvoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierInvoice()
        {
            SupplierInvoiceDetails = new HashSet<SupplierInvoiceDetail>();
            SupplierLedgers = new HashSet<SupplierLedger>();
        }

        public long SupplierInvoiceId { get; set; }

        [StringLength(50)]
        public string SupplierInvoiceNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SupplierInvoiceDate { get; set; }

        [StringLength(50)]
        public string PONumber { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public decimal? TaxAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDueDate { get; set; }

        [StringLength(50)]
        public string PaymentTerms { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DiscountDueDate { get; set; }

        public long SupplierId { get; set; }

        public decimal? FreightCost { get; set; }

        public decimal? DiscountAmount { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierInvoiceDetail> SupplierInvoiceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierLedger> SupplierLedgers { get; set; }
    }
}
