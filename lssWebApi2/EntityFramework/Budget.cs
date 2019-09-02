using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Budget
    {
        public Budget()
        {
            BudgetNote = new HashSet<BudgetNote>();
        }

        public long BudgetId { get; set; }
        public decimal? BudgetHours { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? ActualAmount { get; set; }
        public long? AccountId { get; set; }
        public long? RangeId { get; set; }
        public decimal? ProjectedHours { get; set; }
        public decimal? ProjectedAmount { get; set; }
        public DateTime? ActualsAsOfDate { get; set; }

        public virtual ChartOfAccts Account { get; set; }
        public virtual BudgetRange Range { get; set; }
        public virtual ICollection<BudgetNote> BudgetNote { get; set; }

    }
}