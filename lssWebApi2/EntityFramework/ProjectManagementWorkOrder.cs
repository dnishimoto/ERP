using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EntityFramework
{
    public partial class ProjectManagementWorkOrder
    {
        public long WorkOrderId { get; set; }
        public long? WorkOrderNumber { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? EstimatedHours { get; set; }
        public string AccountNumber { get; set; }
        public string Instructions { get; set; }
        public long ProjectId { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }

        public virtual ProjectManagementProject Project { get; set; }
    }
}
