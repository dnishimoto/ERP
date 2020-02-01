using lssWebApi2.AbstractFactory;
using lssWebApi2.ProjectManagementMilestoneDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.Services;

namespace lssWebApi2.ProjectManagementMilestoneDomain
{
    public class ProjectManagementMilestoneModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentProjectManagementMilestone Milestone;
        public FluentProjectManagementProject Project;

        public ProjectManagementMilestoneModule()
        {
            unitOfWork = new UnitOfWork();
            Milestone = new FluentProjectManagementMilestone(unitOfWork);
            Project = new FluentProjectManagementProject(unitOfWork);
        }
    }
}
