using lssWebApi2.AbstractFactory;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public class PayRollTransactionsByEmployeeModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPayRollTransactionsByEmployee PayRollTransactionsByEmployee;
        public FluentPayRollEarnings PayRollEarnings;
        public FluentPayRollDeductionLiabilities PayRollDeductionLiabilities;
        public FluentEmployee Employee;

        public PayRollTransactionsByEmployeeModule()
        {
            unitOfWork = new UnitOfWork();
            PayRollTransactionsByEmployee = new FluentPayRollTransactionsByEmployee(unitOfWork);
            PayRollEarnings = new FluentPayRollEarnings(unitOfWork);
            PayRollDeductionLiabilities = new FluentPayRollDeductionLiabilities(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
        }
    }
}
