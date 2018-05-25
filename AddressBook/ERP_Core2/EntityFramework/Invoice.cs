namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice
    {
        public long InvoiceId { get; set; }

        [StringLength(20)]
        public string InvoiceNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InvoiceDate { get; set; }

        public decimal? Amount { get; set; }

        public long? BillToAddressId { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public decimal? TaxAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PaymentDueDate { get; set; }

        [StringLength(10)]
        public string PaymentTerms { get; set; }
    }
}
