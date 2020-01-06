using lssWebApi2.EmployeeDomain;
using System.Collections.Generic;
using lssWebApi2.EntityFramework;
using lssWebApi2.TimeAndAttendanceScheduleDomain;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public interface IFluentTimeAndAttendanceScheduledToWork
    {
        IFluentTimeAndAttendanceScheduledToWork Apply();
        IFluentTimeAndAttendanceScheduledToWorkQuery Query();
        IFluentTimeAndAttendanceScheduledToWork AddTimeAndAttendanceScheduledToWork(TimeAndAttendanceScheduledToWork timeAndAttendanceScheduledToWork);
        IFluentTimeAndAttendanceScheduledToWork UpdateTimeAndAttendanceScheduledToWork(TimeAndAttendanceScheduledToWork timeAndAttendanceScheduledToWork);
        IFluentTimeAndAttendanceScheduledToWork DeleteTimeAndAttendanceScheduledToWork(TimeAndAttendanceScheduledToWork timeAndAttendanceScheduledToWork);
        IFluentTimeAndAttendanceScheduledToWork UpdateTimeAndAttendanceScheduledToWorks(IList<TimeAndAttendanceScheduledToWork> newObjects);
        IFluentTimeAndAttendanceScheduledToWork AddTimeAndAttendanceScheduledToWorks(List<TimeAndAttendanceScheduledToWork> newObjects);
        IFluentTimeAndAttendanceScheduledToWork DeleteTimeAndAttendanceScheduledToWorks(List<TimeAndAttendanceScheduledToWork> deleteObjects);
        IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews,string payCode);
    }
}
