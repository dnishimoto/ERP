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
        public long AcctRecId { get; set; }

        [Column(TypeName = "money")]
        public decimal? OpenAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DiscountDueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GLDate { get; set; }

        public long InvoiceId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        public long? DocNumber { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        [StringLength(10)]
        public string NetTerms { get; set; }

        public long CustomerId { get; set; }

        public long? PurchaseOrderId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public long AcctRecDocTypeXRefId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual UDC UDC { get; set; }
    }
}
