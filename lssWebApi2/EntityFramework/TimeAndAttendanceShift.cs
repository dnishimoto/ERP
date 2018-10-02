using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class TimeAndAttendanceShift
    {
        public TimeAndAttendanceShift()
        {
            TimeAndAttendancePunchIn = new HashSet<TimeAndAttendancePunchIn>();
            TimeAndAttendanceSchedule = new HashSet<TimeAndAttendanceSchedule>();
        }

        public long ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int? ShiftStartTime { get; set; }
        public int? ShiftEndTime { get; set; }
        public string ShiftType { get; set; }

        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
        public virtual ICollection<TimeAndAttendanceSchedule> TimeAndAttendanceSchedule { get; set; }
    }
}
