using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.FluentAPI;
using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.ProjectManagementTaskDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.UDCDomain;

namespace lssWebApi2.ProjectManagementDomain
{
    public class ProjectManagementModule : AbstractModule
    {
        public FluentProjectManagementProject Project = new FluentProjectManagementProject();
        public FluentProjectManagementMilestone Milestone = new FluentProjectManagementMilestone();
        public FluentProjectManagementTask Task = new FluentProjectManagementTask();
        public FluentProjectManagementWorkOrder WorkOrder = new FluentProjectManagementWorkOrder();
        public FluentEmployee Employee = new FluentEmployee();
        public FluentProjectManagementWorkOrderToEmployee WorkOrderToEmployee = new FluentProjectManagementWorkOrderToEmployee();
        public FluentProjectManagementTaskToEmployee TaskToEmployee = new FluentProjectManagementTaskToEmployee();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentUdc Udc = new FluentUdc();
        public FluentProjectManagementTask ProjectManagementTask = new FluentProjectManagementTask();
        public FluentProjectManagementMilestone ProjectManagementMilestone = new FluentProjectManagementMilestone();

    }
}
