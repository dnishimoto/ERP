using ERP_Core2.ChartOfAccountsDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentChartOfAccountQuery
    {
        List<ChartOfAccountView> GetChartOfAccountViewsByIds(long[] acctIds);
        List<ChartOfAccountView> GetChartOfAccountViewsByAccount(string company, string busUnit, string objectNumber, string subsidiary);
        ChartOfAccts GetChartofAccount(string company, string busUnit, string objectNumber, string subsidiary);
    }
}
