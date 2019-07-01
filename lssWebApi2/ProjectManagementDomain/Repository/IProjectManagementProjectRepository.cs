using ERP_Core2.ProjectManagementDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain
{
    public interface IProjectManagementProjectRepository
    {
        Task<ProjectManagementProjectView> MapToProjectView(ProjectManagementProject inputObject);
        Task<ProjectManagementTaskView> MapToTaskView(ProjectManagementTask inputObject);
        Task<ProjectManagementWorkOrderView> MapToWorkOrderView(ProjectManagementWorkOrder inputObject);
        Task<ProjectManagementMilestoneView> MaptoMilestoneView(ProjectManagementMilestones inputObject);
        Task<ProjectManagementTaskView> MaptoTaskView(ProjectManagementTask inputObject);
        Task<ProjectManagementProjectView> GetProjectViewById(long projectId);
        Task<ProjectManagementProject> GetProjectById(long projectId);
        Task<ProjectManagementProject> GetProjectByNumber(long projectNumber);
        Task<IQueryable<ProjectManagementWorkOrder>> GetWorkOrdersByProjectId(long projectId);
        Task<IQueryable<ProjectManagementProject>> GetMilestones(long projectId);
        Task<IQueryable<ProjectManagementTask>> GetTasksByProjectId(long projectId);
    }
}
