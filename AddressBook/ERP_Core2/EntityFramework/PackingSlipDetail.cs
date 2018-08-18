namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PackingSlipDetail")]
    public partial class PackingSlipDetail
    {
        public long PackingSlipDetailId { get; set; }

        public long PackingSlipId { get; set; }

        public long ItemId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? ExtendedCost { get; set; }

        [StringLength(20)]
        public string UnitOfMeasure { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual PackingSlip PackingSlip { get; set; }
    }
}
