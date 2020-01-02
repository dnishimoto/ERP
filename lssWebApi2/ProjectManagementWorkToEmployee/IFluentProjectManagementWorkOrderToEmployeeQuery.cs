using lssWebApi2.AutoMapper;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentProjectManagementWorkOrderToEmployeeQuery
{
    Task<ProjectManagementWorkOrderToEmployee> MapToEntity(ProjectManagementWorkOrderToEmployeeView inputObject);
    Task<List<ProjectManagementWorkOrderToEmployee>> MapToEntity(List<ProjectManagementWorkOrderToEmployeeView> inputObjects);
    Task<ProjectManagementWorkOrderToEmployeeView> MapToView(ProjectManagementWorkOrderToEmployee inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ProjectManagementWorkOrderToEmployee> GetEntityById(long? projectManagementWorkOrderToEmployeeId);
    Task<ProjectManagementWorkOrderToEmployee> GetEntityByNumber(long projectManagementWorkOrderToEmployeeNumber);
    Task<ProjectManagementWorkOrderToEmployeeView> GetViewById(long? projectManagementWorkOrderToEmployeeId);
    Task<ProjectManagementWorkOrderToEmployeeView> GetViewByNumber(long projectManagementWorkOrderToEmployeeNumber);
    Task<IList<ProjectManagementWorkOrderToEmployeeView>> GetViewsByWorkOrderId(long? workOrderId);
}
