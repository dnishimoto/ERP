using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollPaySequenceModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollPaySequence PayRollPaySequence;
        public PayRollPaySequenceModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollPaySequence = new FluentPayRollPaySequence(unitOfWork);
        }
    }
}
