using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollCurrentPaySequenceModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollCurrentPaySequence PayRollCurrentPaySequence;
        public PayRollCurrentPaySequenceModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollCurrentPaySequence = new FluentPayRollCurrentPaySequence(unitOfWork);
        }

    }
}
