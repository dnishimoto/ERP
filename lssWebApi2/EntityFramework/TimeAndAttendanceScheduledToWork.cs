using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
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
        public long ShiftId { get; set; }
        public string PayCode { get; set; }
        public string JobCode {get;set;}
        public string WorkedJobCode { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TimeAndAttendanceSchedule Schedule { get; set; }
        public virtual TimeAndAttendanceShift Shift { get; set; }
    }
    }
