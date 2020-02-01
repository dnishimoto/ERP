

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.JobCostTypeDomain
{
public interface IJobCostTypeRepository
    {
        Task<JobCostType> GetEntityById(long ? jobCostTypeId);
	    Task<JobCostType> FindEntityByExpression(Expression<Func<JobCostType, bool>> predicate);
		Task<IList<JobCostType>> FindEntitiesByExpression(Expression<Func<JobCostType, bool>> predicate);
		IQueryable<JobCostType> GetEntitiesByExpression(Expression<Func<JobCostType, bool>> predicate);
    }
}
