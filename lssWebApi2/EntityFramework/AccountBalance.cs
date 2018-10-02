using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class AccountBalance
    {
        public long AccountBalanceId { get; set; }
        public string AccountBalanceType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Hours { get; set; }
        public int FiscalYear { get; set; }
        public int FiscalPeriod { get; set; }
        public long AccountId { get; set; }

        public virtual ChartOfAccts Account { get; set; }
    }
}
