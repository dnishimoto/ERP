namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            AcctPays = new HashSet<AcctPay>();
            AcctRecs = new HashSet<AcctRec>();
            InvoiceDetails = new HashSet<InvoiceDetail>();
            ServiceInformationInvoices = new HashSet<ServiceInformationInvoice>();
        }

        public long InvoiceId { get; set; }

        [StringLength(20)]
        public string InvoiceNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InvoiceDate { get; set; }

        public decimal? Amount { get; set; }

        public long CustomerId { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public decimal? TaxAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDueDate { get; set; }

        [StringLength(10)]
        public string PaymentTerms { get; set; }

        public long CompanyId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcctPay> AcctPays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcctRec> AcctRecs { get; set; }

        public virtual Company Company { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceInformationInvoice> ServiceInformationInvoices { get; set; }
    }
}
