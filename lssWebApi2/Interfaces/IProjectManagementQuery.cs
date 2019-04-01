using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentProjectManagementQuery
    {
        Task<IQueryable<ProjectManagementMilestones>> GetTasksByMilestoneId(long milestoneId);
        Task<IQueryable<ProjectManagementProject>> GetMilestones(long projectId);
        Task<IQueryable<ProjectManagementTask>> GetTasksByProjectId(long projectId);
        Task<IQueryable<ProjectManagementWorkOrder>> GetWorkOrdersByProjectId(long projectId);
        Task<NextNumber> GetProjectNumber();
        Task<ProjectManagementProject> GetProjectByNumber(long projectNumber);
        Task<ProjectManagementWorkOrder> GetWorkOrderByNumber(long workOrderNumber);
        Task<NextNumber> GetWorkOrderNumber();
    }
}
