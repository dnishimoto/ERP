using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class ProjectManagementTask
    {
        public ProjectManagementTask()
        {
            ProjectManagementTaskToEmployee = new HashSet<ProjectManagementTaskToEmployee>();
        }

        public long TaskId { get; set; }
        public string Wbs { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public decimal? EstimatedHours { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? Cost { get; set; }
        public long MileStoneId { get; set; }
        public long StatusXrefId { get; set; }
        public decimal? EstimatedCost { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }
        public long ProjectId { get; set; }
        public string AccountNumber { get; set; }
        public long? WorkOrderId { get; set; }

        public virtual ProjectManagementMilestones MileStone { get; set; }
        public virtual ProjectManagementProject Project { get; set; }
        public virtual Udc StatusXref { get; set; }
        public virtual ICollection<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployee { get; set; }

    }
}
