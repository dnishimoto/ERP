using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Assets
    {
        public long AssetId { get; set; }
        public string AssetCode { get; set; }
        public string TagCode { get; set; }
        public string ClassCode { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? AcquiredDate { get; set; }
        public decimal? OriginalCost { get; set; }
        public decimal? ReplacementCost { get; set; }
        public decimal? Depreciation { get; set; }
        public string Location { get; set; }
        public string SubLocation { get; set; }
        public int? Quantity { get; set; }
        public long EquipmentStatusXrefId { get; set; }
        public string GenericLocationLevel1 { get; set; }
        public string GenericLocationLevel2 { get; set; }
        public string GenericLocationLevel3 { get; set; }

        public virtual Udc EquipmentStatusXref { get; set; }

    }
}
