using lssWebApi2.BudgetDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetRangeDomain
{
    public interface IFluentBudgetRangeQuery
    {
        Task<BudgetRangeView> GetBudgetRange(long? accountId, DateTime? startDate, DateTime? endDate);
        Task<BudgetRange> MapToEntity(BudgetRangeView inputObject);
        Task<IList<BudgetRange>> MapToEntity(IList<BudgetRangeView> inputObjects);
        Task<BudgetRangeView> MapToView(BudgetRange inputObject);
        Task<NextNumber> GetNextNumber();
        Task<BudgetRange> GetEntityById(long ? budgetRangeId);
        Task<BudgetRange> GetEntityByNumber(long budgetRangeNumber);
        Task<BudgetRangeView> GetViewById(long ? budgetRangeId);
        Task<BudgetRangeView> GetViewByNumber(long budgetRangeNumber);
    }
}
