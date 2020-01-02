using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.ProjectManagementDomain;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{
    public class ProjectManagementWorkOrderModule : AbstractModule
    {
        public FluentProjectManagementWorkOrder ProjectManagementWorkOrder = new FluentProjectManagementWorkOrder();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentProjectManagementProject Project = new FluentProjectManagementProject();
    }
}
