using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lssWebApi2.CompanyDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.Services;

namespace lssWebApi2.BudgetRangeDomain
{
    public class BudgetRangeModule : AbstractModule
    {
        private UnitOfWork unitOfWork;

        public FluentBudgetRange BudgetRange;
        public FluentChartOfAccount ChartOfAccount;
        public FluentCompany Company;

        public BudgetRangeModule()
        {
            unitOfWork = new UnitOfWork();
            BudgetRange = new FluentBudgetRange(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            Company = new FluentCompany(unitOfWork);
        }
    }
}
