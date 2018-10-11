using ERP_Core2.AbstractFactory;
using ERP_Core2.BudgetDomain;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentBudgetRangeQuery : AbstractModule, IBudgetRangeQuery
    {
        protected UnitOfWork _unitOfWork;
        public FluentBudgetRangeQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public BudgetRangeView GetBudgetRange(long? accountId, DateTime? startDate, DateTime? endDate)
        {
            Task<BudgetRangeView> budgetRangeViewTask = Task.Run(async () => await _unitOfWork.budgetRangeRepository.GetBudgetRange(accountId, startDate, endDate));
            //Task.WaitAll(budgetRangeViewTask);
            return budgetRangeViewTask.Result;

        }
    }
}
