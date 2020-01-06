using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Linq.Expressions;
using lssWebApi2.AbstractFactory;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public interface IFluentTimeAndAttendanceScheduleQuery
    {
        Task<PageListViewContainer<TimeAndAttendanceScheduleView>> GetViewsByPage(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate, Expression<Func<TimeAndAttendanceSchedule, object>> order, int pageSize, int pageNumber);
        Task<TimeAndAttendanceSchedule> MapToEntity(TimeAndAttendanceScheduleView inputObject);
        Task<IList<TimeAndAttendanceSchedule>> MapToEntity(IList<TimeAndAttendanceScheduleView> inputObjects);
        Task<TimeAndAttendanceScheduleView> MapToView(TimeAndAttendanceSchedule inputObject);
        Task<TimeAndAttendanceScheduleView> GetViewById(long? timePunchinId);
        Task<TimeAndAttendanceSchedule> GetEntityById(long? timePunchinId);
        Task<NextNumber> GetNextNumber();
        Task<TimeAndAttendanceScheduleView> GetViewByNumber(long scheduleNumber);
        Task<TimeAndAttendanceSchedule> GetEntityByNumber(long scheduleNumber);

        Task<TimeAndAttendanceScheduleView> GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate);
    }
}
