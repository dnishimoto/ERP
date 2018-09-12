using ERP_Core2.EntityFramework;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ITimeAndAttendanceScheduledToWork
    {
        ITimeAndAttendanceScheduledToWork Apply();
        ITimeAndAttendanceScheduledToWork AddScheduledToWork(IList<TimeAndAttendanceScheduledToWork> items);
        IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews);
    }
}
