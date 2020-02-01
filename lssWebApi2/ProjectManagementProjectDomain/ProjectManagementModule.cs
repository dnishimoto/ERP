using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.ProjectManagementTaskDomain;
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
        private UnitOfWork unitOfWork;
        public FluentProjectManagementProject Project;
        public FluentProjectManagementMilestone Milestone;
        public FluentProjectManagementTask Task;
        public FluentProjectManagementWorkOrder WorkOrder;
        public FluentEmployee Employee;
        public FluentProjectManagementWorkOrderToEmployee WorkOrderToEmployee;
        public FluentProjectManagementTaskToEmployee TaskToEmployee;
        public FluentChartOfAccount ChartOfAccount;
        public FluentUdc Udc;
        public FluentProjectManagementTask ProjectManagementTask;
        public FluentProjectManagementMilestone ProjectManagementMilestone;


        public ProjectManagementModule()
        {
            unitOfWork = new UnitOfWork();
            Project = new FluentProjectManagementProject(unitOfWork);
            Milestone = new FluentProjectManagementMilestone(unitOfWork);
            Task = new FluentProjectManagementTask(unitOfWork);
            WorkOrder = new FluentProjectManagementWorkOrder(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
            WorkOrderToEmployee = new FluentProjectManagementWorkOrderToEmployee(unitOfWork);
            TaskToEmployee = new FluentProjectManagementTaskToEmployee(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            ProjectManagementTask = new FluentProjectManagementTask(unitOfWork);
            ProjectManagementMilestone = new FluentProjectManagementMilestone(unitOfWork);
        }
    }
}
