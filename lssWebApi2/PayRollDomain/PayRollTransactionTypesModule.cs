using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionTypesModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollTransactionTypes PayRollTransactionTypes;
        public PayRollTransactionTypesModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollTransactionTypes = new FluentPayRollTransactionTypes(unitOfWork);
        }
    }
}
