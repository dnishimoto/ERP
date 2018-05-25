namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Budget")]
    public partial class Budget
    {
        public long BudgetId { get; set; }

        public decimal? BudgetHours { get; set; }

        public decimal? BudgetAmount { get; set; }

        public decimal? ActualHours { get; set; }

        public decimal? ActualAmount { get; set; }

        public long? AccountId { get; set; }

        public long? RangeId { get; set; }

        public decimal? ProjectedHours { get; set; }

        public decimal? ProjectedAmount { get; set; }

        public virtual BudgetRange BudgetRange { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }

        public virtual BudgetRange BudgetRange1 { get; set; }

        public virtual ChartOfAcct ChartOfAcct1 { get; set; }
    }
}
