using ERP_Core2.AccountPayableDomain;
using ERP_Core2.BudgetDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.BudgetsDomain.Repository
{
    public interface IBudgetRangeRepository
    {
        Task<CreateProcessStatus> CreateBudgetRange(BudgetRangeView budgetRangeView);
    }
}
