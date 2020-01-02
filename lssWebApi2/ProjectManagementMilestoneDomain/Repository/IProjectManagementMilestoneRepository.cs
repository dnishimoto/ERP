using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementMilestoneDomain
{
    public interface IProjectManagementMilestoneRepository
    {
        Task<ProjectManagementMilestone> GetEntityById(long ? mileStoneId);
        Task<ProjectManagementMilestone> GetEntityByNumber(long mileStoneNumber);
        Task<IQueryable<ProjectManagementMilestone>> GetEntitiesByProjectId(long? projectId);
        Task<RollupTaskToMilestoneView> GetTaskToMilestoneRollupViewById(long? milestoneId);
    }
}
