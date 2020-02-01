using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lssWebApi2.ChartOfAccountsDomain
{


    public class ChartOfAccountModule : AbstractModule
    {

        private UnitOfWork unitOfWork;
        public FluentChartOfAccount ChartOfAccount;
        public ChartOfAccountModule()
        {
            unitOfWork = new UnitOfWork();
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
        }



    }
}
