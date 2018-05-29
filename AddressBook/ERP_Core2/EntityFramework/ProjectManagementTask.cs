namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectManagementTask")]
    public partial class ProjectManagementTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProjectManagementTask()
        {
            ProjectManagementTaskToEmployees = new HashSet<ProjectManagementTaskToEmployee>();
        }

        [Key]
        public long TaskId { get; set; }

        [StringLength(50)]
        public string WBS { get; set; }

        [StringLength(255)]
        public string TaskName { get; set; }

        [StringLength(2000)]
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

        [StringLength(100)]
        public string AccountNumber { get; set; }

        public virtual ProjectManagementMilestone ProjectManagementMilestone { get; set; }

        public virtual ProjectManagementProject ProjectManagementProject { get; set; }

        public virtual UDC UDC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployees { get; set; }
    }
}
