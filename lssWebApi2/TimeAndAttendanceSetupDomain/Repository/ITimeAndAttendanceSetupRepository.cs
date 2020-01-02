

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{
public interface ITimeAndAttendanceSetupRepository
    {
        Task<TimeAndAttendanceSetup> GetEntityById(long ? timeAndAttendanceSetupId);
	    Task<TimeAndAttendanceSetup> FindEntityByExpression(Expression<Func<TimeAndAttendanceSetup, bool>> predicate);
		Task<IList<TimeAndAttendanceSetup>> FindEntitiesByExpression(Expression<Func<TimeAndAttendanceSetup, bool>> predicate);
		IQueryable<TimeAndAttendanceSetup> GetEntitiesByExpression(Expression<Func<TimeAndAttendanceSetup, bool>> predicate);
    }
}
