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
        [Key]
        [Column(Order = 0)]
        public long PackingSlipDetailId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PackagingSlipId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ItemId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? ExtendedCost { get; set; }

        [StringLength(20)]
        public string UnitOfMeasure { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }

        public virtual PackingSlip PackingSlip { get; set; }
    }
}
