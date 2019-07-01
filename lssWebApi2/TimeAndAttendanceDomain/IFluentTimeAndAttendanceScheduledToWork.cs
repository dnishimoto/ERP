using ERP_Core2.AddressBookDomain;
using ERP_Core2.TimeAndAttendanceDomain;
using System.Collections.Generic;
using lssWebApi2.EntityFramework;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public interface IFluentTimeAndAttendanceScheduledToWork
    {
        IFluentTimeAndAttendanceScheduledToWork Apply();
        IFluentTimeAndAttendanceScheduledToWork AddScheduledToWork(IList<TimeAndAttendanceScheduledToWork> items);
        IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews,string payCode);
    }
}
