using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain
{
    public interface IFluentProjectManagementProjectQuery
    {
        Task<ProjectManagementProject> MapToEntity(ProjectManagementProjectView inputObject);
        Task<List<ProjectManagementProject>> MapToEntity(List<ProjectManagementProjectView> inputObjects);
        Task<ProjectManagementProjectView> MapToView(ProjectManagementProject inputObject);
        Task<NextNumber> GetNextNumber();
        Task<ProjectManagementProject> GetEntityById(long? projectManagementProjectId);
        Task<ProjectManagementProject> GetEntityByNumber(long projectManagementProjectNumber);
        Task<ProjectManagementProjectView> GetViewById(long? projectManagementProjectId);
        Task<ProjectManagementProjectView> GetViewByNumber(long projectManagementProjectNumber);
        Task<RollupTaskToProjectView> GetTaskToProjectRollupViewById(long? projectId);
    }
}
