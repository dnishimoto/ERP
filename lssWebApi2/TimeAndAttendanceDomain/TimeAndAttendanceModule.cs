using ERP_Core2.FluentAPI;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public class TimeAndAttendanceModule 
    {
        public FluentTimeAndAttendance TimeAndAttendance = new FluentTimeAndAttendance();
        public FluentTimeAndAttendanceSchedule TimeAndAttendanceSchedule = new FluentTimeAndAttendanceSchedule();
        public FluentTimeAndAttendanceScheduledToWork TimeAndAttendanceScheduleToWork =new FluentTimeAndAttendanceScheduledToWork();
    }
}
