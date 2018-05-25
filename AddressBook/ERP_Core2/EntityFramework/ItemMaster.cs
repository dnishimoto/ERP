namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemMaster")]
    public partial class ItemMaster
    {
        [Key]
        public long ItemId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(20)]
        public string UnitOfMeasure { get; set; }

        [StringLength(10)]
        public string CommodityCode { get; set; }

        [StringLength(20)]
        public string ItemPriceGroup { get; set; }

        [StringLength(255)]
        public string Description2 { get; set; }

        [StringLength(20)]
        public string ItemNumber { get; set; }

        public virtual Inventory Inventory { get; set; }
    }
}
