namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcctRec")]
    public partial class AcctRec
    {
        public long Id { get; set; }

        [StringLength(10)]
        public string DocType { get; set; }

        [Column(TypeName = "money")]
        public decimal? OpenAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DiscountDueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GLDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InvoiceDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public long? DocNumber { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [StringLength(10)]
        public string NetTerms { get; set; }

        public long AddressId { get; set; }

        public long? ItemId { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string PONumber { get; set; }
    }
}
