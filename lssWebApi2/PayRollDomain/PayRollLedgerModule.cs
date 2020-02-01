using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollLedgerModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollLedger PayRollLedger;
        public FluentPayRollPaySequence PayRollPaySequence;
        public FluentPayRollTransactionsByEmployee PayRollTransactionsByEmployee;
        public FluentPayRollCurrentPaySequence PayRollCurrentPaySequence;

        public PayRollLedgerModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollLedger = new FluentPayRollLedger(unitOfWork);
            PayRollPaySequence = new FluentPayRollPaySequence(unitOfWork);
            PayRollTransactionsByEmployee = new FluentPayRollTransactionsByEmployee(unitOfWork);
            PayRollCurrentPaySequence = new FluentPayRollCurrentPaySequence(unitOfWork);
        }
    }
}
