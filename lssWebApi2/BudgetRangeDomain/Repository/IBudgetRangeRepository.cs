using lssWebApi2.AccountPayableDomain;
using lssWebApi2.BudgetDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetRangeDomain.Repository
{
    public interface IBudgetRangeRepository
    {
        Task<BudgetRange> GetBudgetRange(long? accountId, DateTime? startDate, DateTime? endDate);
        Task<BudgetRange> GetEntityById(long ? budgetRangeId);
        Task<BudgetRange> FindEntityByExpression(Expression<Func<BudgetRange, bool>> predicate);
        Task<IList<BudgetRange>> FindEntitiesByExpression(Expression<Func<BudgetRange, bool>> predicate);
    }
}
