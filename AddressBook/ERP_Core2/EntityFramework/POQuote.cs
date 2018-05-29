namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("POQuote")]
    public partial class POQuote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POQuote()
        {
            AcctPays = new HashSet<AcctPay>();
        }

        public long POQuoteId { get; set; }

        [Column(TypeName = "money")]
        public decimal? QuoteAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SubmittedDate { get; set; }

        public long PoId { get; set; }

        public long DocNumber { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public long CustomerId { get; set; }

        public long SupplierId { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AcctPay> AcctPays { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
