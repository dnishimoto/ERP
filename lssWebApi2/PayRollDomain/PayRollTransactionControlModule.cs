using lssWebApi2.AbstractFactory;
using lssWebApi2.PayRollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionControlModule : AbstractModule
    {
        public FluentPayRollTransactionControl PayRollTransactionControl = new FluentPayRollTransactionControl();
        public FluentPayRollEarnings PayRollEarnings = new FluentPayRollEarnings();
        public FluentPayRollDeductionLiabilities PayRollDeductionLiabilities = new FluentPayRollDeductionLiabilities();
    }
}
