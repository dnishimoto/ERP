using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollTransactionsByEmployee
    {
        public long PayRollTransactionsByEmployeeId { get; set; }
        public long Employee { get; set; }
        public long PayRollTransactionCode { get; set; }
        public decimal Amount { get; set; }
        public decimal? TaxPercentOfGross { get; set; }
        public decimal? AdditionalAmount { get; set; }

    }
}