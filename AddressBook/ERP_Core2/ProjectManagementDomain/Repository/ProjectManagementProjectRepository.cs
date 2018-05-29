using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.Services;


namespace MillenniumERP.ProjectManagementDomain
{

    public class ProjectManagementProjectRepository: Repository<ProjectManagementProject>
    {
        Entities _dbContext;
        public ProjectManagementProjectRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
        public async Task<IQueryable<ProjectManagementProject>> GetMilestones(int projectId)
        {
    
            var list = await base.GetObjectsAsync(e => e.ProjectId == projectId, "ProjectManagementMilestones").ToListAsync();

            return list.AsQueryable<ProjectManagementProject>();

        }
        public async Task<IQueryable<ProjectManagementTask>> GetTasksByProjectId(int projectId)
        {
            var list = await (from milestones in _dbContext.ProjectManagementMilestones
                              join tasks in _dbContext.ProjectManagementTasks on milestones.MilestoneId equals tasks.MileStoneId
                              where (milestones.ProjectId == projectId)
                              select tasks).ToListAsync<ProjectManagementTask>();

            return list.AsQueryable<ProjectManagementTask>();


        }
        /*
        public async Task<IQueryable<ProjectManagementMilestone>> GetMilestones(int projectId)
        {
           
            var list = await base.GetObjectsAsync(e => e.ProjectId == projectId, "ProjectManagmentMilestone").ToListAsync();

            return list.AsQueryable<ProjectManagementMilestone>();
           
        }
        */
    }
}
