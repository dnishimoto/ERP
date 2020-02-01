using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EmployeeDomain
{
    public class EmployeeModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentEmployee Employee;
        public EmployeeModule()
        {
            unitOfWork = new UnitOfWork();
            Employee = new FluentEmployee(unitOfWork);
        }
    }
}
