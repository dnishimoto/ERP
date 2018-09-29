using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IBudget
    {
        IBudget TransactBudget(BudgetView budgetView);
        IBudget Apply();
        IBudget MapRangeToBudgetView(ref BudgetView budgetView, BudgetRangeView budgetRangeView);
        IBudgetQuery Query();
    }
}
