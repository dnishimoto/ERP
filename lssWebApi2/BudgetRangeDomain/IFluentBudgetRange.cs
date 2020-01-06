using lssWebApi2.BudgetDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetRangeDomain
{
    public interface IFluentBudgetRange
    {
        IFluentBudgetRange CreateBudgetRange(BudgetRangeView budgetRange);
        IFluentBudgetRange Apply();
        IFluentBudgetRangeQuery Query();
        IFluentBudgetRange AddBudgetRange(BudgetRange budgetRange);
        IFluentBudgetRange UpdateBudgetRange(BudgetRange budgetRange);
        IFluentBudgetRange DeleteBudgetRange(BudgetRange budgetRange);
        IFluentBudgetRange UpdateBudgetRanges(IList<BudgetRange> newObjects);
        IFluentBudgetRange AddBudgetRanges(List<BudgetRange> newObjects);
        IFluentBudgetRange DeleteBudgetRanges(List<BudgetRange> deleteObjects);
    }
}
