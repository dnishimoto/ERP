﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class HumanResourcesSalary
    {
        public long HumanResourcesSalaryId { get; set; }
        public long Employee { get; set; }
        public decimal? AnnualizedSalary { get; set; }
        public decimal? HourlyRate { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}