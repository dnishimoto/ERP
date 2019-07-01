using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.BudgetDomain
{
    public interface IFluentBudgetQuery
    {
        BudgetActualsView GetBudgetActualsView(BudgetRangeView budgetRangeView);
        BudgetView GetBudgetView(long budgetId);
        IEnumerable<BudgetView> GetBudgetViews();
        List<PersonalBudgetView> GetPersonalBudgetViews();
    }
}
