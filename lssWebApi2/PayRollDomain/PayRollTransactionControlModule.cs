using lssWebApi2.AbstractFactory;
using lssWebApi2.PayRollDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionControlModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollTransactionControl PayRollTransactionControl;
        public FluentPayRollEarnings PayRollEarnings;
        public FluentPayRollDeductionLiabilities PayRollDeductionLiabilities;

        public PayRollTransactionControlModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollTransactionControl = new FluentPayRollTransactionControl(unitOfWork);
            PayRollEarnings = new FluentPayRollEarnings(unitOfWork);
            PayRollDeductionLiabilities = new FluentPayRollDeductionLiabilities(unitOfWork);
        }
    }
}
