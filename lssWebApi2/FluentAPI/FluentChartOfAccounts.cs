using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentChartOfAccounts : AbstractErrorHandling, IChartOfAccounts
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        FluentChartOfAccountsQuery _query = null;
        public FluentChartOfAccounts()
        {
        }

        public IChartOfAccountsQuery Query()
        {
            if (_query == null) { _query = new FluentChartOfAccountsQuery(unitOfWork); }
            return _query as IChartOfAccountsQuery;
        }

    }
}
