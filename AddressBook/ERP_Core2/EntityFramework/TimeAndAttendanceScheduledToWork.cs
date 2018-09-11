namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeAndAttendanceScheduledToWork")]
    public partial class TimeAndAttendanceScheduledToWork
    {
        [Key]
        public long ScheduledToWorkId { get; set; }

        public long EmployeeId { get; set; }

        public long ScheduleId { get; set; }

        [StringLength(255)]
        public string ScheduleName { get; set; }

        [StringLength(20)]
        public string StartDateTime { get; set; }

        [StringLength(20)]
        public string EndDateTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [StringLength(255)]
        public string EmployeeName { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TimeAndAttendanceSchedule TimeAndAttendanceSchedule { get; set; }
    }
}
