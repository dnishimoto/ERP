using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetDomain
{
    public interface IFluentBudget
    {
        IFluentBudget TransactBudget(BudgetView budgetView);
        IFluentBudget Apply();
        IFluentBudget MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView);
        IFluentBudgetQuery Query();
        IFluentBudget AddBudgets(List<Budget> newObjects);
        IFluentBudget UpdateBudgets(IList<Budget> newObjects);
        IFluentBudget AddBudget(Budget newObject);
        IFluentBudget UpdateBudget(Budget updateObject);
        IFluentBudget DeleteBudget(Budget deleteObject);
        IFluentBudget DeleteBudgets(List<Budget> deleteObjects);
       
    }
}
