using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.Services;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{
    public class ProjectManagementWorkOrderModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentProjectManagementWorkOrder ProjectManagementWorkOrder;
        public FluentChartOfAccount ChartOfAccount;
        public FluentProjectManagementProject Project;

        public ProjectManagementWorkOrderModule()
        {
            unitOfWork = new UnitOfWork();
            ProjectManagementWorkOrder = new FluentProjectManagementWorkOrder(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            Project = new FluentProjectManagementProject(unitOfWork);
        }
    }
}
