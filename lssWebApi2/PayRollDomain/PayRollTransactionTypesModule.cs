using lssWebApi2.AbstractFactory;
using lssWebApi2.PayRollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionTypesModule : AbstractModule
    {
        public FluentPayRollTransactionTypes PayRollTransactionTypes = new FluentPayRollTransactionTypes();
    }
}