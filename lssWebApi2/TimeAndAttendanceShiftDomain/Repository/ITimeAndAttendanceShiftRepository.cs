

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.TimeAndAttendanceShiftDomain
{
public interface ITimeAndAttendanceShiftRepository
    {
        Task<TimeAndAttendanceShift> GetEntityById(long ? timeAndAttendanceShiftId);
	    Task<TimeAndAttendanceShift> FindEntityByExpression(Expression<Func<TimeAndAttendanceShift, bool>> predicate);
		Task<IList<TimeAndAttendanceShift>> FindEntitiesByExpression(Expression<Func<TimeAndAttendanceShift, bool>> predicate);
		IQueryable<TimeAndAttendanceShift> GetEntitiesByExpression(Expression<Func<TimeAndAttendanceShift, bool>> predicate);
    }
}
