using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class TimeAndAttendanceSchedule
    {
        public TimeAndAttendanceSchedule()
        {
            TimeAndAttendancePunchIn = new HashSet<TimeAndAttendancePunchIn>();
            TimeAndAttendanceScheduledToWork = new HashSet<TimeAndAttendanceScheduledToWork>();
        }

        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ShiftId { get; set; }
        public string ScheduleGroup { get; set; }

        public virtual TimeAndAttendanceShift Shift { get; set; }
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
        public virtual ICollection<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWork { get; set; }
    }
}
