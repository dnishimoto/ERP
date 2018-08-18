namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inventory")]
    public partial class Inventory
    {
        public long InventoryId { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [StringLength(2000)]
        public string Remarks { get; set; }

        [StringLength(100)]
        public string UnitOfMeasure { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? ExtendedPrice { get; set; }

        public long? DistributionAccountId { get; set; }

        public long? PackingSlipDetailId { get; set; }

        public long ItemId { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }
    }
}
