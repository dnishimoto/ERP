using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementTaskDomain
{
    public interface IProjectManagementTaskRepository
    {
        Task<ProjectManagementTask> GetEntityById(long? taskId);
        Task<ProjectManagementTask> GetEntityByNumber(long taskNumber);
        Task<IQueryable<ProjectManagementTask>> GetEntitiesByProjectId(long? projectId);
        Task<IQueryable<ProjectManagementTask>> GetEntitiesByMilestoneId(long? milestoneId);
    }
}
