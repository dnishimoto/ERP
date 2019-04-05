using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class ProjectManagementMilestones
    {
        public ProjectManagementMilestones()
        {
            ProjectManagementTask = new HashSet<ProjectManagementTask>();
        }

        public long MilestoneId { get; set; }
        public long MileStoneNumber { get; set; }
        public string MilestoneName { get; set; }
        public long? ProjectId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public decimal? Cost { get; set; }
        public string Wbs { get; set; }

        public virtual ProjectManagementProject Project { get; set; }
        public virtual ICollection<ProjectManagementTask> ProjectManagementTask { get; set; }

    }
}
