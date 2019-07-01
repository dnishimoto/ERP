using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Linq.Expressions;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public interface IFluentTimeAndAttendanceScheduleQuery
    {
        TimeAndAttendanceScheduleView GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate);
    }
}
