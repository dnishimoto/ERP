using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollEarningsModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollEarnings PayRollEarnings;
        public PayRollEarningsModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollEarnings = new FluentPayRollEarnings(unitOfWork);
        }
    }
}
