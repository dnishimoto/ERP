using lssWebApi2.ProjectManagementDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain
{
    public interface IProjectManagementProjectRepository
    {

        Task<ProjectManagementProject> GetEntityById(long ? projectId);
        Task<ProjectManagementProject> GetEntityByNumber(long projectNumber);
        Task<RollupTaskToProjectView> GetTaskToProjectRollupViewById(long? projectId);

    }
}
