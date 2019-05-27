using ERP_Core2.AccountPayableDomain;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceDomain.Repository
{
    public interface ITimeAndAttendanceRepository
    {
        TimeAndAttendancePunchInView MapToView(TimeAndAttendancePunchIn item);
        Task<TimeAndAttendanceViewContainer> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber);
        Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long employeeId, int hours, int minutes, int mealDurationInMinutes, DateTime workDay, string account);
        Task<TimeAndAttendancePunchInView> GetPunchInByIdView(long timePunchinId);
        Task<TimeAndAttendancePunchInView> GetPunchOpenView(long employeeId);
        Task<TimeAndAttendancePunchIn> GetPunchOpen(long employeeId);
        Task<TimeAndAttendancePunchIn> IsPunchOpen(long employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchIn> BuildPunchin(long employeeId, string account, DateTime punchDate);
        Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate);
        Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByDate(DateTime startDate, DateTime endDate);
        Task<TimeAndAttendanceShift> GetShiftById(long shiftId);
        Task<List<TimeAndAttendancePunchInView>> GetTAPunchinByEmployeeId(long employeeId);
        CreateProcessStatus DeletePunchin(TimeAndAttendancePunchIn taPunchin);
        Task<CreateProcessStatus> AddPunchin(TimeAndAttendancePunchIn taPunchin);
        Task<CreateProcessStatus> UpdateByTimePunchinId(long timePunchinId, int workDurationInMinutes, int mealDurationInMinutes);
        DateTime GetPunchDateTime(string s24Hrs);
    }
}
