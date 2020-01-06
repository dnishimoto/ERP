using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetDomain
{
    public interface IFluentBudgetQuery
    {
        Task<BudgetActualsView> GetBudgetActualsView(BudgetRangeView budgetRangeView);
        Task<BudgetView> GetBudgetView(long budgetId);
        Task<IList<PersonalBudgetView>> GetPersonalBudgetViews();
        Task<Budget> MapToEntity(BudgetView inputObject);
        Task<IList<Budget>> MapToEntity(IList<BudgetView> inputObjects);
        Task<BudgetView> MapToView(Budget inputObject);
        Task<NextNumber> GetNextNumber();
        Task<Budget> GetEntityById(long ? budgetId);
        Task<Budget> GetEntityByNumber(long budgetNumber);
        Task<BudgetView> GetViewById(long ? budgetId);
        Task<BudgetView> GetViewByNumber(long budgetNumber);
        Task<IList<BudgetView>> GetBudgetViews();
    }
}
