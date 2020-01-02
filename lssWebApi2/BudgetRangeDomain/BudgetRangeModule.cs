using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.CompanyDomain;
using lssWebApi2.ChartOfAccountsDomain;

namespace lssWebApi2.BudgetRangeDomain
{
    public class BudgetRangeModule : AbstractModule
    {
        public FluentBudgetRange BudgetRange = new FluentBudgetRange();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentCompany Company = new FluentCompany();
    }
}
