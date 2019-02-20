﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class TimeAndAttendanceShift
    {
        public TimeAndAttendanceShift()
        {
            TimeAndAttendancePunchIn = new HashSet<TimeAndAttendancePunchIn>();
            TimeAndAttendanceSchedule = new HashSet<TimeAndAttendanceSchedule>();
            TimeAndAttendanceScheduledToWork = new HashSet<TimeAndAttendanceScheduledToWork>();
        }

        public long ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int? ShiftStartTime { get; set; }
        public int? ShiftEndTime { get; set; }
        public string ShiftType { get; set; }
        public int DurationHours { get; set; }
        public int DurationMinutes { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }

        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
        public virtual ICollection<TimeAndAttendanceSchedule> TimeAndAttendanceSchedule { get; set; }
        public virtual ICollection<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWork { get; set; }

    }
}
