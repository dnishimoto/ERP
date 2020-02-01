

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.JobCostLedgerDomain
{
public interface IJobCostLedgerRepository
    {
        Task<JobCostLedger> GetEntityById(long ? jobCostLedgerId);
	    Task<JobCostLedger> FindEntityByExpression(Expression<Func<JobCostLedger, bool>> predicate);
		Task<IList<JobCostLedger>> FindEntitiesByExpression(Expression<Func<JobCostLedger, bool>> predicate);
		IQueryable<JobCostLedger> GetEntitiesByExpression(Expression<Func<JobCostLedger, bool>> predicate);
    }
}
