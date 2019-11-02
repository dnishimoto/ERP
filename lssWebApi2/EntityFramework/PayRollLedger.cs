﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollLedger
    {
        public long PayRollLedgerId { get; set; }
        public long EmployeeId { get; set; }
        public long PayRollTransactionCode { get; set; }
        public string PayRollType { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime PayPeriodBegin { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public int PayRollGroupCode { get; set; }
        public string ReversingEntry { get; set; }
        public string UpdateEntry { get; set; }
        public long PayRollLedgerNumber { get; set; }
        public long PaySequence { get; set; }
        public string TransactionType { get; set; }

    }
}