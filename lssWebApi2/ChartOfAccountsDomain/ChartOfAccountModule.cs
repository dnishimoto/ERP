using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.FluentAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.ChartOfAccountsDomain
{

   
    public class ChartOfAccountModule : AbstractModule
    {

        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();

      
    }
}
