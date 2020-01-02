using lssWebApi2.EntityFramework;
using lssWebApi2.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public interface IFluentTimeAndAttendanceScheduledToWorkQuery
    {
        Task<TimeAndAttendanceScheduledToWork> MapToEntity(TimeAndAttendanceScheduledToWorkView inputObject);
        Task<List<TimeAndAttendanceScheduledToWork>> MapToEntity(List<TimeAndAttendanceScheduledToWorkView> inputObjects);
        Task<TimeAndAttendanceScheduledToWorkView> MapToView(TimeAndAttendanceScheduledToWork inputObject);
        Task<TimeAndAttendanceScheduledToWorkView> GetViewById(long? scheduledToWorkId);
        Task<TimeAndAttendanceScheduledToWork> GetEntityById(long? scheduledToWorkId);
        Task<TimeAndAttendanceScheduledToWorkView> GetViewByNumber(long scheduledToWorkNumber);
        Task<NextNumber> GetNextNumber();
        Task<TimeAndAttendanceScheduledToWork> GetEntityByNumber(long scheduledToWorkNumber);
    }
}
