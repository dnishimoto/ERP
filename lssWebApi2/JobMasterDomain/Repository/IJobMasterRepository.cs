

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.JobMasterDomain
{
public interface IJobMasterRepository
    {
        Task<JobMaster> GetEntityById(long ? jobMasterId);
	    Task<JobMaster> FindEntityByExpression(Expression<Func<JobMaster, bool>> predicate);
		Task<IList<JobMaster>> FindEntitiesByExpression(Expression<Func<JobMaster, bool>> predicate);
		IQueryable<JobMaster> GetEntitiesByExpression(Expression<Func<JobMaster, bool>> predicate);
    }
}
