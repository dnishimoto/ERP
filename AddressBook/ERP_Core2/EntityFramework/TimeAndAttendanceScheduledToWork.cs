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

        public virtual TimeAndAttendanceSchedule TimeAndAttendanceSchedule { get; set; }
    }
}
