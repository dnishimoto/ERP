using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain.Repository
{
    public interface IProjectManagementMilestoneRepository
    {
        Task<ProjectManagementMilestones> GetMileStoneById(long mileStoneId);
        Task<ProjectManagementMilestones> GetMileStoneByNumber(long mileStoneNumber);
        Task<IQueryable<ProjectManagementMilestones>> GetTasksByMilestoneId(long milestoneId);
    }
}
