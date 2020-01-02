using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.ProjectManagementTaskDomain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public interface IFluentProjectManagementTaskQuery
{
    Task<ProjectManagementTask> MapToEntity(ProjectManagementTaskView inputObject);
    Task<List<ProjectManagementTask>> MapToEntity(List<ProjectManagementTaskView> inputObjects);
    Task<ProjectManagementTaskView> MapToView(ProjectManagementTask inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ProjectManagementTask> GetEntityById(long? projectManagementTaskId);
    Task<ProjectManagementTask> GetEntityByNumber(long projectManagementTaskNumber);
    Task<ProjectManagementTaskView> GetViewById(long? projectManagementTaskId);
    Task<ProjectManagementTaskView> GetViewByNumber(long projectManagementTaskNumber);
    Task<IQueryable<ProjectManagementTask>> GetEntitiesByMilestoneId(long ? milestoneId);
    Task<IQueryable<ProjectManagementTask>> GetEntitiesByProjectId(long? projectId);
}
