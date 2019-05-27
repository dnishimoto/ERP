using ERP_Core2.AccountPayableDomain;
using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetsDomain.Repository
{
    public interface IBudgetRepository
    {
        Task<BudgetView> GetBudgetView(long budgetId);
        Task<BudgetActualsView> GetActualsView(BudgetRangeView budgetRangeView);
        void MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView);
        Task<CreateProcessStatus> TransactBudget(BudgetView budgetView);
    }
}
