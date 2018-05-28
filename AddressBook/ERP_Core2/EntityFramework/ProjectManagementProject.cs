namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectManagementProject")]
    public partial class ProjectManagementProject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectManagementProject()
        {
            ProjectManagementMilestones = new HashSet<ProjectManagementMilestone>();
            ProjectManagementTasks = new HashSet<ProjectManagementTask>();
        }

        [Key]
        public long ProjectId { get; set; }

        [StringLength(255)]
        public string ProjectName { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [StringLength(2000)]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectManagementMilestone> ProjectManagementMilestones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectManagementTask> ProjectManagementTasks { get; set; }
    }
}
