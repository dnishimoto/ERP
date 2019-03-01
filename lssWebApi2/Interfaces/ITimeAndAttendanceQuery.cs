using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace ERP_Core2.Interfaces
{
    public interface IFluentTimeAndAttendanceQuery
    {
        Task<TimeAndAttendancePunchIn> BuildPunchin(long employeeId,string account);
        Task<TimeAndAttendancePunchIn> GetPunchInById(long timePunchinId);
        Task<bool> IsPunchOpen(long employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchIn> GetPunchOpen(long employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchInView> GetPunchOpenView(long employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long employeeId, int hours, int minutes, DateTime workDay, string account);

        Task<IList<TimeAndAttendancePunchInView>> GetTAPunchinByEmployeeId(long employeeId);
        TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate);

        Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByDate(DateTime startDate, DateTime endDate);
        Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate);
        Task<IPagedList<TimeAndAttendancePunchIn>> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber);

        Task<TimeAndAttendanceTimeView> GetUTCAdjustedTime();

    }

}
