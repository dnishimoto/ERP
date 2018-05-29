namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectManagementTaskToEmployee")]
    public partial class ProjectManagementTaskToEmployee
    {
        [Key]
        public long TaskToEmployeeId { get; set; }

        public long? EmployeeId { get; set; }

        public long? TaskId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ProjectManagementTask ProjectManagementTask { get; set; }
    }
}
