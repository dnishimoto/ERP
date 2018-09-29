using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
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

        public ProjectManagementMilestones MileStone { get; set; }
        public ProjectManagementProject Project { get; set; }
        public Udc StatusXref { get; set; }
        public ICollection<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployee { get; set; }
    }
}
