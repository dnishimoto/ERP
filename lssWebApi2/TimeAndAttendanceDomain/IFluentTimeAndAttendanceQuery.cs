using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public interface IFluentTimeAndAttendanceQuery
    {
        Task<PageListViewContainer<TimeAndAttendancePunchInView>> GetViewsByPage(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate, Expression<Func<TimeAndAttendancePunchIn, object>> order, int pageSize, int pageNumber);
        Task<TimeAndAttendancePunchIn> MapToEntity(TimeAndAttendancePunchInView inputObject);
        Task<List<TimeAndAttendancePunchIn>> MapToEntity(List<TimeAndAttendancePunchInView> inputObjects);
        Task<TimeAndAttendancePunchInView> MapToView(TimeAndAttendancePunchIn inputObject);
        Task<TimeAndAttendancePunchInView> GetViewById(long? timePunchinId);
        Task<TimeAndAttendancePunchIn> GetEntityById(long? timePunchinId);
        Task<TimeAndAttendancePunchIn> BuildPunchin(long ?employeeId,string account,DateTime punchDate);
        Task<TimeAndAttendancePunchIn> IsPunchOpen(long ? employeeId, DateTime asOfDate);
        Task<TimeAndAttendancePunchIn> GetPunchOpen(long ? employeeId);
        Task<TimeAndAttendancePunchInView> GetPunchOpenView(long ? employeeId);
        Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long ? employeeId, int hours, int minutes, int mealDurationInMinutes, DateTime workDay, string account);
        Task<IList<TimeAndAttendancePunchInView>> GetEntitiesByEmployeeId(long ? employeeId);
        TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate);
        Task<IList<TimeAndAttendanceView>> GetViewsByDate(DateTime startDate, DateTime endDate);
        Task<IList<TimeAndAttendanceView>> GetViewsByIdAndDate(long ? employeeId, DateTime startDate, DateTime endDate);
        Task<TimeAndAttendanceTimeView> GetUTCAdjustedTime();
        }

}
