using lssWebApi2.AccountPayableDomain;
using lssWebApi2.BudgetDomain;
using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetsDomain.Repository
{
    public interface IBudgetRepository
    {
        Task<IList<PersonalBudgetView>> GetPersonalBudgetViews();
        Task<BudgetActualsView> GetActualsView(BudgetRangeView budgetRangeView);
        Task<Budget> GetEntityById(long ? budgetId);
        Task<Budget> FindEntityByExpression(Expression<Func<Budget, bool>> predicate);
        Task<IList<Budget>> FindEntitiesByExpression(Expression<Func<Budget, bool>> predicate);
        Task<IList<Budget>> GetBudgets();
    }
}
