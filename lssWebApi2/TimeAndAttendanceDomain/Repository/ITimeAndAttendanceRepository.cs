using lssWebApi2.AccountPayableDomain;
using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public interface ITimeAndAttendanceRepository
    {
        Task<TimeAndAttendancePunchIn> GetEntityById(long? timePunchinId);
        IQueryable<TimeAndAttendancePunchIn> GetEntitiesByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate);
        Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long ? employeeId, int hours, int minutes, int mealDurationInMinutes, DateTime workDay, string account);
        Task<IList<TimeAndAttendancePunchIn>> GetEntitiesByEmployeeId(long? employeeId);
        Task<IList<TimeAndAttendancePunchIn>> GetOpenEntitiesByEmployeeId(long? employeeId);
        Task<TimeAndAttendancePunchIn> GetPunchOpen(long ? employeeId);
        Task<TimeAndAttendancePunchIn> IsPunchOpen(long ? employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchIn> BuildPunchin(long ? employeeId, string account, DateTime punchDate);
        Task<IList<TimeAndAttendanceView>> GetViewsByIdAndDate(long  ? employeeId, DateTime startDate, DateTime endDate);
        Task<IList<TimeAndAttendanceView>> GetViewsByDate(DateTime startDate, DateTime endDate);
        Task<TimeAndAttendanceShift> GetShiftById(long ? shiftId);
        CreateProcessStatus DeletePunchin(TimeAndAttendancePunchIn taPunchin);
        Task<CreateProcessStatus> AddPunchin(TimeAndAttendancePunchIn taPunchin);
        Task<CreateProcessStatus> UpdateByTimePunchinId(long ? timePunchinId, int workDurationInMinutes, int mealDurationInMinutes);
        DateTime GetPunchDateTime(string s24Hrs);
    }
}
