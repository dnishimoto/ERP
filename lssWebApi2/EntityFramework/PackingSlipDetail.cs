using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PackingSlipDetail
    {
        public long PackingSlipDetailId { get; set; }
        public long PackingSlipId { get; set; }
        public long ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ExtendedCost { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Description { get; set; }

        public virtual PackingSlip PackingSlip { get; set; }

    }
}