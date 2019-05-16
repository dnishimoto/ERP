using ERP_Core2.AddressBookDomain;
using ERP_Core2.ProjectManagementDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.ProjectManagementDomain.Repository;
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
        Task<ProjectManagementWorkOrder> GetWorkOrderById(long workOrderId);
        Task<ProjectManagementProject> GetProjectById(long projectId);
        Task<NextNumber> GetWorkOrderNumber();
        Task<NextNumber> GetMileStoneNumber();
        Task<IEnumerable<EmployeeView>> GetEmployeeByWorkOrderId(long workOrderId);
        Task<ProjectManagementTaskView> MaptoTaskView(ProjectManagementTask inputObject);
        Task<ProjectManagementMilestoneView> MaptoMilestoneView(ProjectManagementMilestones inputObject);
        Task<ProjectManagementWorkOrderView> MapToWorkOrderView(ProjectManagementWorkOrder inputObject);
        Task<ProjectManagementTaskView> MapToTaskView(ProjectManagementTask inputObject);
        Task<ProjectManagementProjectView> MapToProjectView(ProjectManagementProject inputObject);
        Task<ProjectManagementMilestones> GetMileStoneByNumber(long mileStoneNumber);
        Task<ProjectManagementMilestones> GetMileStoneById(long mileStoneId);
    }
}
