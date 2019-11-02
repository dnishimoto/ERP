using ERP_Core2.AbstractFactory;
using ERP_Core2.PayRollDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionsByEmployeeModule : AbstractModule
    {
        public FluentPayRollTransactionsByEmployee PayRollTransactionsByEmployee = new FluentPayRollTransactionsByEmployee();
    }
}
