﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class PayRollEmployeeBenefit
    {
        public long PayrollEmployeeBenefitsId { get; set; }
        public long Employee { get; set; }
        public string BenefitCode { get; set; }
        public decimal Amount { get; set; }
        public long TransactionCode { get; set; }
        public decimal? Percentage { get; set; }
        public string Frequency { get; set; }
        public int? BenefitOption { get; set; }

    }
}