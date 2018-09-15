using ERP_Core2.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ITimeAndAttendanceSchedule
    {
        ITimeAndAttendanceSchedule AddSchedule(TimeAndAttendanceScheduleView view);
        ITimeAndAttendanceSchedule Apply();
        ITimeAndAttendanceScheduleQuery Query();

    }
}
