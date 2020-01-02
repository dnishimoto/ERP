using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementMilestoneDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.ProjectManagementDomain;

namespace lssWebApi2.ProjectManagementMilestoneDomain
{
    public class ProjectManagementMilestoneModule : AbstractModule
    {
        public FluentProjectManagementMilestone Milestone = new FluentProjectManagementMilestone();
        public FluentProjectManagementProject Project = new FluentProjectManagementProject();
    }
}
