namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcctPay")]
    public partial class AcctPay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcctPay()
        {
            SupplierLedgers = new HashSet<SupplierLedger>();
        }

        public long AcctPayId { get; set; }

        public long? DocNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal? GrossAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscountAmount { get; set; }

        public string Remark { get; set; }

        public DateTime? GLDate { get; set; }

        public long SupplierId { get; set; }

        public long? ContractId { get; set; }

        public long? POQuoteId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public long? PurchaseOrderId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }

        public long? InvoiceId { get; set; }

        public long AccountId { get; set; }

        [Required]
        [StringLength(20)]
        public string DocType { get; set; }

        [StringLength(20)]
        public string PaymentTerms { get; set; }

        public decimal? DiscountPercent { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmountOpen { get; set; }

        [StringLength(50)]
        public string OrderNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DiscountDueDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmountPaid { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual POQuote POQuote { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierLedger> SupplierLedgers { get; set; }
    }
}
