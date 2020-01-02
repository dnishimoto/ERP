using lssWebApi2.EmployeeDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceScheduleDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public interface ITimeAndAttendanceScheduledToWorkRepository
    {
        Task<CreateProcessStatus> AddScheduledToWorkItems(IList<TimeAndAttendanceScheduledToWork> items);
        TimeAndAttendanceScheduledToWork BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, TimeAndAttendanceScheduleDayView dayView, EmployeeView employeeItem, string payCode);
        Task<TimeAndAttendanceScheduledToWork> GetEntityById(long? scheduledToWorkId);
        Task<TimeAndAttendanceScheduledToWork> GetEntityByNumber(long scheduledToWorkNumber);
    }
}
