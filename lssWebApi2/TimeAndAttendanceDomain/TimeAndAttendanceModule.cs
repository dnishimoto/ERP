using lssWebApi2.Services;
using lssWebApi2.TimeAndAttendanceScheduleDomain;
using lssWebApi2.TimeAndAttendanceScheduledToWorkDomain;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public class TimeAndAttendanceModule
    {
        private UnitOfWork unitOfWork;
        public FluentTimeAndAttendance TimeAndAttendance;
        public FluentTimeAndAttendanceSchedule Schedule;
        public FluentTimeAndAttendanceScheduledToWork ScheduleToWork;

        public TimeAndAttendanceModule()
        {
            unitOfWork = new UnitOfWork();
            TimeAndAttendance = new FluentTimeAndAttendance(unitOfWork);
            Schedule = new FluentTimeAndAttendanceSchedule(unitOfWork);
            ScheduleToWork = new FluentTimeAndAttendanceScheduledToWork(unitOfWork);
        }
    }
}
