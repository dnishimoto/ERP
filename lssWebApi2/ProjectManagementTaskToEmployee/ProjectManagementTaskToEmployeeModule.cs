using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.ProjectManagementTaskDomain;
using lssWebApi2.EmployeeDomain;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{
    public class ProjectManagementTaskToEmployeeModule : AbstractModule
    {
        public FluentProjectManagementTaskToEmployee ProjectManagementTaskToEmployee = new FluentProjectManagementTaskToEmployee();
        public FluentProjectManagementProject Project = new FluentProjectManagementProject();
        public FluentProjectManagementMilestone Milestone = new FluentProjectManagementMilestone();
        public FluentProjectManagementTask Task = new FluentProjectManagementTask();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentEmployee Employee = new FluentEmployee();
    }
}
