using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.FluentAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain
{

   
    public class ChartOfAccountModule : AbstractModule
    {

        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();

      
    }
}
