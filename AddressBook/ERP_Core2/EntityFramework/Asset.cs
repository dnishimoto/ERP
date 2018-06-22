namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Asset
    {
        public long AssetId { get; set; }

        [StringLength(50)]
        public string AssetCode { get; set; }

        [StringLength(50)]
        public string TagCode { get; set; }

        [StringLength(50)]
        public string ClassCode { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Manufacturer { get; set; }

        [StringLength(50)]
        public string Model { get; set; }

        [StringLength(50)]
        public string SerialNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AcquiredDate { get; set; }

        public decimal? OriginalCost { get; set; }

        public decimal? ReplacementCost { get; set; }

        public decimal? Depreciation { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(50)]
        public string SubLocation { get; set; }

        public int? Quantity { get; set; }

        public long EquipmentStatusXRefId { get; set; }

        [StringLength(50)]
        public string GenericLocationLevel1 { get; set; }

        [StringLength(50)]
        public string GenericLocationLevel2 { get; set; }

        [StringLength(50)]
        public string GenericLocationLevel3 { get; set; }

        public virtual UDC UDC { get; set; }
    }
}
