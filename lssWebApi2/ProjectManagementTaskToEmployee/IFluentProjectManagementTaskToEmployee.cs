

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{ 

public interface IFluentProjectManagementTaskToEmployee
    {
        IFluentProjectManagementTaskToEmployeeQuery Query();
        IFluentProjectManagementTaskToEmployee Apply();
        IFluentProjectManagementTaskToEmployee AddProjectManagementTaskToEmployee(ProjectManagementTaskToEmployee projectManagementTaskToEmployee);
        IFluentProjectManagementTaskToEmployee UpdateProjectManagementTaskToEmployee(ProjectManagementTaskToEmployee projectManagementTaskToEmployee);
        IFluentProjectManagementTaskToEmployee DeleteProjectManagementTaskToEmployee(ProjectManagementTaskToEmployee projectManagementTaskToEmployee);
     	IFluentProjectManagementTaskToEmployee UpdateProjectManagementTaskToEmployees(IList<ProjectManagementTaskToEmployee> newObjects);
        IFluentProjectManagementTaskToEmployee AddProjectManagementTaskToEmployees(List<ProjectManagementTaskToEmployee> newObjects);
        IFluentProjectManagementTaskToEmployee DeleteProjectManagementTaskToEmployees(List<ProjectManagementTaskToEmployee> deleteObjects);
    }
}
