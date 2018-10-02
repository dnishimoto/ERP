using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class PackingSlip
    {
        public PackingSlip()
        {
            PackingSlipDetail = new HashSet<PackingSlipDetail>();
        }

        public long PackingSlipId { get; set; }
        public long SupplierId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string SlipDocument { get; set; }
        public string Ponumber { get; set; }
        public string Remark { get; set; }
        public string SlipType { get; set; }
        public decimal? Amount { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PackingSlipDetail> PackingSlipDetail { get; set; }
    }
}
