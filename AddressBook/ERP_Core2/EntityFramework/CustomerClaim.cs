namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerClaim")]
    public partial class CustomerClaim
    {
        [Key]
        public long ClaimId { get; set; }

        public long ClassificationXRefId { get; set; }

        public long CustomerId { get; set; }

        public string Configuration { get; set; }

        public string Note { get; set; }

        public long EmployeeId { get; set; }

        public long GroupIdXrefId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProcessedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual UDC UDC { get; set; }

        public virtual UDC UDC1 { get; set; }
    }
}
