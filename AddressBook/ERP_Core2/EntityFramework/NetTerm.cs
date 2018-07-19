namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NetTerm
    {
        public long NetTermId { get; set; }

        [StringLength(50)]
        public string NetTerms { get; set; }

        public decimal? DiscountPercent { get; set; }
    }
}
