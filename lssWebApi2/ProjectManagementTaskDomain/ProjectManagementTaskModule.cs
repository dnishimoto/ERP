using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementTaskDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using lssWebApi2.Services;

namespace lssWebApi2.ProjectManagementTaskDomain
{
    public class ProjectManagementTaskModule : AbstractModule
    {
        private UnitOfWork unitOfWork;

        public FluentProjectManagementTask ProjectManagementTask;
        public FluentProjectManagementMilestone Milestone;
        public FluentUdc Udc;
        public FluentProjectManagementProject Project;
        public FluentProjectManagementWorkOrder WorkOrder;
        public FluentChartOfAccount ChartOfAccount;
        public FluentProjectManagementWorkOrderToEmployee WorkToEmployee;


        public ProjectManagementTaskModule()
        {
            unitOfWork = new UnitOfWork();
            ProjectManagementTask = new FluentProjectManagementTask(unitOfWork);
            Milestone = new FluentProjectManagementMilestone(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            Project = new FluentProjectManagementProject(unitOfWork);
            WorkOrder = new FluentProjectManagementWorkOrder(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            WorkToEmployee = new FluentProjectManagementWorkOrderToEmployee(unitOfWork);
        }
    }
}
