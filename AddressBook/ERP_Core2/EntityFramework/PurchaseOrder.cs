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

        [StringLength(10)]
        public string POType { get; set; }

        [StringLength(10)]
        public string PaymentTerms { get; set; }

        [Column(TypeName = "money")]
        public decimal? GrossAmount { get; set; }

        public string Remark { get; set; }

        public DateTime? GLDate { get; set; }

        public long AccountId { get; set; }

        public long SupplierId { get; set; }

        public long CustomerId { get; set; }

        public long? ContractId { get; set; }

        public long? POQuoteId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string PONumber { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string UnitOfMeasure { get; set; }

        [StringLength(10)]
        public string TakenBy { get; set; }

        public long? ShippedToLocationId { get; set; }

        public long? BuyerId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PromisedDeliveredDate { get; set; }

        public decimal? Tax { get; set; }

        [StringLength(10)]
        public string TaxCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcctPay> AcctPays { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
