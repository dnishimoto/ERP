using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Inventory
    {
        public long InventoryId { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string UnitOfMeasure { get; set; }
        public int? Quantity { get; set; }
        public decimal? ExtendedPrice { get; set; }
        public long? DistributionAccountId { get; set; }
        public long? PackingSlipDetailId { get; set; }
        public long ItemId { get; set; }

        public virtual ItemMaster Item { get; set; }

    }
}
