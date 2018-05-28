namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProjectManagementMilestone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectManagementMilestone()
        {
            ProjectManagementTasks = new HashSet<ProjectManagementTask>();
        }

        [Key]
        public long MilestoneId { get; set; }

        [StringLength(255)]
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

        [StringLength(50)]
        public string WBS { get; set; }

        public virtual ProjectManagementProject ProjectManagementProject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectManagementTask> ProjectManagementTasks { get; set; }
    }
}
