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
        [Key]
        public long ItemId { get; set; }

        [StringLength(100)]
        public string ShortDescription { get; set; }

        [StringLength(255)]
        public string LongDescription { get; set; }

        [StringLength(2000)]
        public string Remarks { get; set; }

        [StringLength(100)]
        public string UOM { get; set; }

        [StringLength(100)]
        public string SKU { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal? ExtendedPrice { get; set; }

        public long? DistributionAccountId { get; set; }

        public long? ReceivingAccountId { get; set; }

        public virtual ItemMaster ItemMaster { get; set; }
    }
}
