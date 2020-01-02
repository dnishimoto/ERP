using lssWebApi2.TimeAndAttendanceScheduleDomain;
using lssWebApi2.TimeAndAttendanceScheduledToWorkDomain;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public class TimeAndAttendanceModule 
    {
        public FluentTimeAndAttendance TimeAndAttendance = new FluentTimeAndAttendance();
        public FluentTimeAndAttendanceSchedule Schedule = new FluentTimeAndAttendanceSchedule();
        public FluentTimeAndAttendanceScheduledToWork ScheduleToWork =new FluentTimeAndAttendanceScheduledToWork();
    }
}
