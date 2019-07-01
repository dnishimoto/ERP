using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.BudgetDomain
{
    public interface IFluentBudgetRange
    {
        IFluentBudgetRange CreateBudgetRange(BudgetRangeView budgetRange);
        IFluentBudgetRange Apply();
        IFluentBudgetRangeQuery Query();
    }
}
