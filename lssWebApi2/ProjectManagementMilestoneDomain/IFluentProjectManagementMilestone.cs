

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ProjectManagementMilestoneDomain;

namespace lssWebApi2.ProjectManagementMilestoneDomain
{ 

public interface IFluentProjectManagementMilestone
    {
        IFluentProjectManagementMilestoneQuery Query();
        IFluentProjectManagementMilestone Apply();
        IFluentProjectManagementMilestone AddProjectManagementMilestone(ProjectManagementMilestone projectManagementMilestone);
        IFluentProjectManagementMilestone UpdateProjectManagementMilestone(ProjectManagementMilestone projectManagementMilestone);
        IFluentProjectManagementMilestone DeleteProjectManagementMilestone(ProjectManagementMilestone projectManagementMilestone);
     	IFluentProjectManagementMilestone UpdateProjectManagementMilestones(IList<ProjectManagementMilestone> newObjects);
        IFluentProjectManagementMilestone AddProjectManagementMilestones(List<ProjectManagementMilestone> newObjects);
        IFluentProjectManagementMilestone DeleteProjectManagementMilestones(List<ProjectManagementMilestone> deleteObjects);
    }
}
