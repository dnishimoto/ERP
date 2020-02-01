

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.JobPhaseDomain
{
public interface IJobPhaseRepository
    {
        Task<JobPhase> GetEntityById(long ? jobPhaseId);
	    Task<JobPhase> FindEntityByExpression(Expression<Func<JobPhase, bool>> predicate);
		Task<IList<JobPhase>> FindEntitiesByExpression(Expression<Func<JobPhase, bool>> predicate);
		IQueryable<JobPhase> GetEntitiesByExpression(Expression<Func<JobPhase, bool>> predicate);
        Task<IList<JobPhase>> GetEntitiesByJobMasterId(long? jobMasterId);
    }
}
