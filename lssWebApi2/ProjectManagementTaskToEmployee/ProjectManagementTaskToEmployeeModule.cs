using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.ProjectManagementTaskDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{
    public class ProjectManagementTaskToEmployeeModule : AbstractModule
    {
        private UnitOfWork unitOfWork;

        public FluentProjectManagementTaskToEmployee ProjectManagementTaskToEmployee;
        public FluentProjectManagementProject Project;
        public FluentProjectManagementMilestone Milestone;
        public FluentProjectManagementTask Task;
        public FluentAddressBook AddressBook;
        public FluentEmployee Employee;

        public ProjectManagementTaskToEmployeeModule()
        {
            unitOfWork = new UnitOfWork();
            ProjectManagementTaskToEmployee = new FluentProjectManagementTaskToEmployee(unitOfWork);
            Project = new FluentProjectManagementProject(unitOfWork);
            Milestone = new FluentProjectManagementMilestone(unitOfWork);
            Task = new FluentProjectManagementTask(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
        }
    }
}
