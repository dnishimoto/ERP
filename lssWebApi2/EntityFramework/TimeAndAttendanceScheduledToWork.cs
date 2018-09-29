using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class TimeAndAttendanceScheduledToWork
    {
        public long ScheduledToWorkId { get; set; }
        public long EmployeeId { get; set; }
        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EmployeeName { get; set; }

        public Employee Employee { get; set; }
        public TimeAndAttendanceSchedule Schedule { get; set; }
    }
}
