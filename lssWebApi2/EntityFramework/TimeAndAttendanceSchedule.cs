﻿using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
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
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }

        public virtual TimeAndAttendanceShift Shift { get; set; }
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
        public virtual ICollection<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWork { get; set; }

    }
}
