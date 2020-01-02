using lssWebApi2.BudgetRangeDomain;
using lssWebApi2.FluentAPI;
using lssWebApi2.ChartOfAccountsDomain;
namespace lssWebApi2.BudgetDomain
{


    public class BudgetModule
    {
        public FluentBudget Budget = new FluentBudget();
        public FluentBudgetRange BudgetRange = new FluentBudgetRange();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
    }
}
