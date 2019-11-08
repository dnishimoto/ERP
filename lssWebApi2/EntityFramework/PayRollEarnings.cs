using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollEarnings
    {
        public long PayRollEarningsId { get; set; }
        public int EarningCode { get; set; }
        public string Description { get; set; }
        public string EarningType { get; set; }
        public long PayRollEarningsNumber { get; set; }

    }
}