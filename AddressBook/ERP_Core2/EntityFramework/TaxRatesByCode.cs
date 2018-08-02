namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaxRatesByCode")]
    public partial class TaxRatesByCode
    {
        [Key]
        public long TaxId { get; set; }

        [StringLength(20)]
        public string TaxCode { get; set; }

        [Column(TypeName = "money")]
        public decimal? TaxRate { get; set; }

        [StringLength(2)]
        public string State { get; set; }
    }
}
