using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{
    public class ProjectManagementWorkOrderToEmployeeModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentProjectManagementWorkOrderToEmployee ProjectManagementWorkOrderToEmployee;
        public FluentAddressBook AddressBook;
        public FluentEmployee Employee;
        public FluentProjectManagementWorkOrder WorkOrder;

        public ProjectManagementWorkOrderToEmployeeModule()
        {
            unitOfWork = new UnitOfWork();
            ProjectManagementWorkOrderToEmployee = new FluentProjectManagementWorkOrderToEmployee(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
            WorkOrder = new FluentProjectManagementWorkOrder(unitOfWork);
        }
    }
}
