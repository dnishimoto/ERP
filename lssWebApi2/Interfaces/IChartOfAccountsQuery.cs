using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IChartOfAccountsQuery
    {
      
        ChartOfAccts GetChartofAccount(string company, string busUnit, string objectNumber, string subsidiary);
    }
}
