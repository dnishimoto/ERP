using lssWebApi2.BudgetRangeDomain;

using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.Services;
namespace lssWebApi2.BudgetDomain
{


    public class BudgetModule
    {
        private UnitOfWork unitOfWork;
        public FluentBudget Budget;
        public FluentBudgetRange BudgetRange;
        public FluentChartOfAccount ChartOfAccount;
        public BudgetModule()
        {
            unitOfWork = new UnitOfWork();
            Budget = new FluentBudget(unitOfWork);
            BudgetRange = new FluentBudgetRange(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
        }
    }
}
