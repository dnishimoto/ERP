﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollTransactionControl
    {
        public long PayRollTransactionControlId { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
        public string PayRollType { get; set; }
        public decimal? RateAmount { get; set; }
        public string RateType { get; set; }
        public int PayRollTransactionCode { get; set; }
        public decimal? UpperLimit1 { get; set; }
        public decimal? UpperLimit2 { get; set; }

    }
}