using ERP_Core2.AddressBookDomain;
using ERP_Core2.TimeAndAttendanceDomain;
using System.Collections.Generic;
using lssWebApi2.EntityFramework;

namespace ERP_Core2.Interfaces
{
    public interface ITimeAndAttendanceScheduledToWork
    {
        ITimeAndAttendanceScheduledToWork Apply();
        ITimeAndAttendanceScheduledToWork AddScheduledToWork(IList<TimeAndAttendanceScheduledToWork> items);
        IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews,string payCode);
    }
}
