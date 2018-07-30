namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseOrder")]
    public partial class PurchaseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PurchaseOrder()
        {
            AcctPays = new HashSet<AcctPay>();
            PurchaseOrderDetails = new HashSet<PurchaseOrderDetail>();
        }

        public long PurchaseOrderId { get; set; }

        [StringLength(20)]
        public string DocType { get; set; }

        [StringLength(10)]
        public string PaymentTerms { get; set; }

        [Column(TypeName = "money")]
        public decimal? GrossAmount { get; set; }

        public string Remark { get; set; }

        public DateTime? GLDate { get; set; }

        public long AccountId { get; set; }

        public long SupplierId { get; set; }

        public long? ContractId { get; set; }

        public long? POQuoteId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string PONumber { get; set; }

        [StringLength(10)]
        public string TakenBy { get; set; }

        public long? BuyerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PromisedDeliveredDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }

        [StringLength(10)]
        public string TaxCode { get; set; }

        public DateTime? TransactionDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmountReceived { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmountPaid { get; set; }

        [StringLength(255)]
        public string ShippedToName { get; set; }

        [StringLength(100)]
        public string ShippedToAddress1 { get; set; }

        [StringLength(100)]
        public string ShippedToAddress2 { get; set; }

        [StringLength(50)]
        public string ShippedToCity { get; set; }

        [StringLength(20)]
        public string ShippedToZipcode { get; set; }

        [StringLength(20)]
        public string ShippedToState { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcctPay> AcctPays { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
