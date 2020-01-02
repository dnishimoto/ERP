using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public interface ITimeAndAttendanceScheduleRepository
    {
        IQueryable<TimeAndAttendanceSchedule> GetEntitiesByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate);
        Task<TimeAndAttendanceSchedule> GetEntityById(long? timePunchinId);
        Task<TimeAndAttendanceSchedule> GetEntityByNumber(long scheduleNumber);
    }
}
