using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceDomain.Repository
{
    public interface ITimeAndAttendanceScheduledToWorkRepository
    {
        Task<CreateProcessStatus> AddScheduledToWorkItems(IList<TimeAndAttendanceScheduledToWork> items);
        TimeAndAttendanceScheduledToWork BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, TimeAndAttendanceScheduleDayView dayView, EmployeeView employeeItem, string payCode);
    }
}
