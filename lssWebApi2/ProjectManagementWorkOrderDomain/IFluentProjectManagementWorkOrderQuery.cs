using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ProjectManagementWorkOrderDomain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public interface IFluentProjectManagementWorkOrderQuery
{
    Task<ProjectManagementWorkOrder> MapToEntity(ProjectManagementWorkOrderView inputObject);
    Task<IList<ProjectManagementWorkOrder>> MapToEntity(IList<ProjectManagementWorkOrderView> inputObjects);
    Task<ProjectManagementWorkOrderView> MapToView(ProjectManagementWorkOrder inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ProjectManagementWorkOrder> GetEntityById(long? projectManagementWorkOrderId);
    Task<ProjectManagementWorkOrder> GetEntityByNumber(long projectManagementWorkOrderNumber);
    Task<ProjectManagementWorkOrderView> GetViewById(long? projectManagementWorkOrderId);
    Task<ProjectManagementWorkOrderView> GetViewByNumber(long projectManagementWorkOrderNumber);
    Task<IQueryable<ProjectManagementWorkOrder>> GetEntitiesByProjectId(long ? projectId);
}
