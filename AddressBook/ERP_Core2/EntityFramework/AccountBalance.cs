//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MillenniumERP.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccountBalance
    {
        public long AccountBalanceId { get; set; }
        public string AccountBalanceType { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Hours { get; set; }
        public int FiscalYear { get; set; }
        public int FiscalPeriod { get; set; }
        public long AccountId { get; set; }
    
        public virtual ChartOfAcct ChartOfAcct { get; set; }
    }
}