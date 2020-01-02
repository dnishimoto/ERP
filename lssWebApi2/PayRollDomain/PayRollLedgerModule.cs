using lssWebApi2.AbstractFactory;
using lssWebApi2.PayRollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollLedgerModule : AbstractModule
    {
        public FluentPayRollLedger PayRollLedger = new FluentPayRollLedger();
        public FluentPayRollPaySequence PayRollPaySequence = new FluentPayRollPaySequence();
        public FluentPayRollTransactionsByEmployee PayRollTransactionsByEmployee = new FluentPayRollTransactionsByEmployee();
        public FluentPayRollCurrentPaySequence PayRollCurrentPaySequence = new FluentPayRollCurrentPaySequence();
    }
}
