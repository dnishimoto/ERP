using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.ProjectManagementWorkOrderDomain;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{
    public class ProjectManagementWorkOrderToEmployeeModule : AbstractModule
    {
        public FluentProjectManagementWorkOrderToEmployee ProjectManagementWorkOrderToEmployee = new FluentProjectManagementWorkOrderToEmployee();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentEmployee Employee = new FluentEmployee();
        public FluentProjectManagementWorkOrder WorkOrder = new FluentProjectManagementWorkOrder();
        
    }
}
