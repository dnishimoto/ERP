using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.BudgetDomain
{
    public interface IFluentBudget
    {
        IFluentBudget TransactBudget(BudgetView budgetView);
        IFluentBudget Apply();
        IFluentBudget MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView);
        IFluentBudgetQuery Query();
    }
}
