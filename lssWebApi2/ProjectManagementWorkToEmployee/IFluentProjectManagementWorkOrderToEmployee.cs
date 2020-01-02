

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{ 

public interface IFluentProjectManagementWorkOrderToEmployee
    {
        IFluentProjectManagementWorkOrderToEmployeeQuery Query();
        IFluentProjectManagementWorkOrderToEmployee Apply();
        IFluentProjectManagementWorkOrderToEmployee AddProjectManagementWorkOrderToEmployee(ProjectManagementWorkOrderToEmployee projectManagementWorkOrderToEmployee);
        IFluentProjectManagementWorkOrderToEmployee UpdateProjectManagementWorkOrderToEmployee(ProjectManagementWorkOrderToEmployee projectManagementWorkOrderToEmployee);
        IFluentProjectManagementWorkOrderToEmployee DeleteProjectManagementWorkOrderToEmployee(ProjectManagementWorkOrderToEmployee projectManagementWorkOrderToEmployee);
     	IFluentProjectManagementWorkOrderToEmployee UpdateProjectManagementWorkOrderToEmployees(List<ProjectManagementWorkOrderToEmployee> newObjects);
        IFluentProjectManagementWorkOrderToEmployee AddProjectManagementWorkOrderToEmployees(List<ProjectManagementWorkOrderToEmployee> newObjects);
        IFluentProjectManagementWorkOrderToEmployee DeleteProjectManagementWorkOrderToEmployees(List<ProjectManagementWorkOrderToEmployee> deleteObjects);
    }
}
