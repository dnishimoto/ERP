using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTotalsModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollTotals PayRollTotals;
        public PayRollTotalsModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollTotals = new FluentPayRollTotals(unitOfWork);
        }
    }
}
