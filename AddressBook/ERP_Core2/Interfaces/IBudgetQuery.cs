using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IBudgetQuery
    {
        BudgetActualsView GetBudgetActuals(BudgetRangeView budgetRangeView);
    }
}
