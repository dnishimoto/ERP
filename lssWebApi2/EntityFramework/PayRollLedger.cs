using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollLedger
    {
        public long PayrollLedgerId { get; set; }
        public long EmployeeId { get; set; }
        public long PayRollTransactionCode { get; set; }
        public string PayRollType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime PayPeriodBegin { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public string TransactionType { get; set; }
        public int PayRollGroupCode { get; set; }

    }
}