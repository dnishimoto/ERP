using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementTaskDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ProjectManagementMilestoneDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;

namespace lssWebApi2.ProjectManagementTaskDomain
{
    public class ProjectManagementTaskModule : AbstractModule
    {
        public FluentProjectManagementTask ProjectManagementTask = new FluentProjectManagementTask();
        public FluentProjectManagementMilestone Milestone = new FluentProjectManagementMilestone();
        public FluentUdc Udc = new FluentUdc();
        public FluentProjectManagementProject Project = new FluentProjectManagementProject();
        public FluentProjectManagementWorkOrder WorkOrder = new FluentProjectManagementWorkOrder();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentProjectManagementWorkOrderToEmployee WorkToEmployee = new FluentProjectManagementWorkOrderToEmployee();
    }
}
