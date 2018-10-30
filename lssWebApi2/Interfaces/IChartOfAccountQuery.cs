using ERP_Core2.ChartOfAccountsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IChartOfAccountQuery
    {
        List<ChartOfAccountView> GetChartOfAccountViewsByIds(long[] acctIds);
        List<ChartOfAccountView> GetChartOfAccountViewsByAccount(string company, string busUnit, string objectNumber, string subsidiary);
    }
}
