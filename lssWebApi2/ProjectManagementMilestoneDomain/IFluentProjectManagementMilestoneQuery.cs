using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.ProjectManagementMilestoneDomain;

public interface IFluentProjectManagementMilestoneQuery
{
    Task<ProjectManagementMilestone> MapToEntity(ProjectManagementMilestoneView inputObject);
    Task<IList<ProjectManagementMilestone>> MapToEntity(IList<ProjectManagementMilestoneView> inputObjects);
    Task<ProjectManagementMilestoneView> MapToView(ProjectManagementMilestone inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ProjectManagementMilestone> GetEntityById(long? projectManagementMilestoneId);
    Task<ProjectManagementMilestone> GetEntityByNumber(long projectManagementMilestoneNumber);
    Task<ProjectManagementMilestoneView> GetViewById(long? projectManagementMilestoneId);
    Task<ProjectManagementMilestoneView> GetViewByNumber(long projectManagementMilestoneNumber);
    Task<IQueryable<ProjectManagementMilestone>> GetEntitiesByProjectId(long projectId);
    Task<RollupTaskToMilestoneView> GetTaskToMilestoneRollupViewById(long? milestoneId);
}
