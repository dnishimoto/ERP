using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public interface IFluentTimeAndAttendanceSchedule
    {
        IFluentTimeAndAttendanceSchedule Apply();
        IFluentTimeAndAttendanceScheduleQuery Query();
        IFluentTimeAndAttendanceSchedule AddTimeAndAttendanceSchedule(TimeAndAttendanceSchedule timeAndAttendanceSchedule);
        IFluentTimeAndAttendanceSchedule UpdateTimeAndAttendanceSchedule(TimeAndAttendanceSchedule timeAndAttendanceSchedule);
        IFluentTimeAndAttendanceSchedule DeleteTimeAndAttendanceSchedule(TimeAndAttendanceSchedule timeAndAttendanceSchedule);
        IFluentTimeAndAttendanceSchedule UpdateTimeAndAttendanceSchedules(IList<TimeAndAttendanceSchedule> newObjects);
        IFluentTimeAndAttendanceSchedule AddTimeAndAttendanceSchedules(List<TimeAndAttendanceSchedule> newObjects);
        IFluentTimeAndAttendanceSchedule DeleteTimeAndAttendanceSchedules(List<TimeAndAttendanceSchedule> deleteObjects);
    }
}
