using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain.Repository
{
    public interface IProjectManagementTaskRepository
    {
        Task<IQueryable<ProjectManagementTask>> GetEmployeeByTaskId(int taskId);
    }
}
