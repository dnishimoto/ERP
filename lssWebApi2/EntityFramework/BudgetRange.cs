using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class BudgetRange
    {
        public BudgetRange()
        {
            Budget = new HashSet<Budget>();
        }

        public long RangeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string GenCode { get; set; }
        public string SubCode { get; set; }
        public string CompanyCode { get; set; }
        public string BusinessUnit { get; set; }
        public string Subsidiary { get; set; }
        public long? AccountId { get; set; }
        public string SupervisorCode { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string ObjectNumber { get; set; }
        public bool? IsActive { get; set; }

        public ChartOfAccts Account { get; set; }
        public ICollection<Budget> Budget { get; set; }
    }
}
