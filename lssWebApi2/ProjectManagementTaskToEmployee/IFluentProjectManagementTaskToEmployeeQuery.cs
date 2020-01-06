using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementTaskToEmployeeDomain;

public interface IFluentProjectManagementTaskToEmployeeQuery
{
    Task<ProjectManagementTaskToEmployee> MapToEntity(ProjectManagementTaskToEmployeeView inputObject);
    Task<IList<ProjectManagementTaskToEmployee>> MapToEntity(IList<ProjectManagementTaskToEmployeeView> inputObjects);
    Task<ProjectManagementTaskToEmployeeView> MapToView(ProjectManagementTaskToEmployee inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ProjectManagementTaskToEmployee> GetEntityById(long? projectManagementTaskToEmployeeId);
    Task<ProjectManagementTaskToEmployee> GetEntityByNumber(long projectManagementTaskToEmployeeNumber);
    Task<ProjectManagementTaskToEmployeeView> GetViewById(long? projectManagementTaskToEmployeeId);
    Task<ProjectManagementTaskToEmployeeView> GetViewByNumber(long projectManagementTaskToEmployeeNumber);
}
