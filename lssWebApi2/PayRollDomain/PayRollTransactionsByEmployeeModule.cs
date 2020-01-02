using lssWebApi2.AbstractFactory;
using lssWebApi2.EmployeeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionsByEmployeeModule : AbstractModule
    {
        public FluentPayRollTransactionsByEmployee PayRollTransactionsByEmployee = new FluentPayRollTransactionsByEmployee();
        public FluentPayRollEarnings PayRollEarnings = new FluentPayRollEarnings();
        public FluentPayRollDeductionLiabilities PayRollDeductionLiabilities = new FluentPayRollDeductionLiabilities();
        public FluentEmployee Employee = new FluentEmployee();
    }
}
