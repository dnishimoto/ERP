using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ProjectManagementProject
    {
        public ProjectManagementProject()
        {
            ProjectManagementMilestones = new HashSet<ProjectManagementMilestones>();
            ProjectManagementTask = new HashSet<ProjectManagementTask>();
        }

        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public decimal? EstimatedHours { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public decimal? Cost { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }

        public virtual ICollection<ProjectManagementMilestones> ProjectManagementMilestones { get; set; }
        public virtual ICollection<ProjectManagementTask> ProjectManagementTask { get; set; }
    }
}
