using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentProjectManagementQuery: IFluentProjectManagementQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentProjectManagementQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IQueryable<ProjectManagementMilestones>> GetTasksByMilestoneId(long milestoneId)
        {
            try
            {
                IQueryable<ProjectManagementMilestones> query = await _unitOfWork.projectManagementMilestoneRepository.GetTasksByMilestoneId(milestoneId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetTasksByMilestoneId", ex);
            }
        }
        public async Task<IQueryable<ProjectManagementProject>> GetMilestones(long projectId)
        {
            try
            {
                IQueryable<ProjectManagementProject> query = await _unitOfWork.projectManagementProjectRepository.GetMilestones(projectId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetMilestones", ex);
            }
        }

        public async Task<IQueryable<ProjectManagementTask>> GetTasksByProjectId(long projectId)
        {
            try
            {
                IQueryable<ProjectManagementTask> query = await _unitOfWork.projectManagementProjectRepository.GetTasksByProjectId(projectId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetTasksByProjectId", ex);
            }
        }
        public async Task<IQueryable<ProjectManagementWorkOrder>> GetWorkOrdersByProjectId(long projectId)
        {
            try
            {
                IQueryable<ProjectManagementWorkOrder> query = await _unitOfWork.projectManagementProjectRepository.GetWorkOrdersByProjectId(projectId);
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception("GetWorkOrdersByProjectId", ex);
            }

        }
    }
}
