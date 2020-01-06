

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ProjectManagementTaskDomain;

namespace lssWebApi2.ProjectManagementTaskDomain
{ 

public interface IFluentProjectManagementTask
    {
        IFluentProjectManagementTaskQuery Query();
        IFluentProjectManagementTask Apply();
        IFluentProjectManagementTask AddProjectManagementTask(ProjectManagementTask projectManagementTask);
        IFluentProjectManagementTask UpdateProjectManagementTask(ProjectManagementTask projectManagementTask);
        IFluentProjectManagementTask DeleteProjectManagementTask(ProjectManagementTask projectManagementTask);
     	IFluentProjectManagementTask UpdateProjectManagementTasks(IList<ProjectManagementTask> newObjects);
        IFluentProjectManagementTask AddProjectManagementTasks(List<ProjectManagementTask> newObjects);
        IFluentProjectManagementTask DeleteProjectManagementTasks(List<ProjectManagementTask> deleteObjects);
    }
}
