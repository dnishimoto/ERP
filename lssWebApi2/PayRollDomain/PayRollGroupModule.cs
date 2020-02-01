using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollGroupModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollGroup PayRollGroup;
        public PayRollGroupModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollGroup = new FluentPayRollGroup(unitOfWork);
        }
    }
}
