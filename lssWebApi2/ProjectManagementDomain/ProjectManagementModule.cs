using ERP_Core2.AbstractFactory;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_Core2.ProjectManagementDomain
{
    public class ProjectManagementModule : AbstractModule
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public async Task<IQueryable<ProjectManagementMilestones>> GetTasksByMilestoneId(long milestoneId)
        {
            try
            { 
            IQueryable<ProjectManagementMilestones> query = await unitOfWork.projectManagementMilestoneRepository.GetTasksByMilestoneId(milestoneId);
            return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<IQueryable<ProjectManagementProject>> GetMilestones(long projectId)
        {
            try { 
            IQueryable<ProjectManagementProject> query = await unitOfWork.projectManagementProjectRepository.GetMilestones(projectId);
            return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        public async Task<IQueryable<ProjectManagementTask>> GetTasksByProjectId(long projectId)
        {
            try { 
            IQueryable<ProjectManagementTask> query = await unitOfWork.projectManagementProjectRepository.GetTasksByProjectId(projectId);
            return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
