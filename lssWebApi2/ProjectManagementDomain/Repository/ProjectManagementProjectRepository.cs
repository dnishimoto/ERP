using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ProjectManagementDomain
{

    public class ProjectManagementProjectRepository: Repository<ProjectManagementProject>
    {
        ListensoftwareDBContext _dbContext;
        public ProjectManagementProjectRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
        }
        public async Task<IQueryable<ProjectManagementProject>> GetMilestones(long projectId)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.ProjectId == projectId, "ProjectManagementMilestones").ToListAsync();

                return list.AsQueryable<ProjectManagementProject>();
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<IQueryable<ProjectManagementTask>> GetTasksByProjectId(long projectId)
        {
            try
            {
                var list = await (from milestones in _dbContext.ProjectManagementMilestones
                                  join tasks in _dbContext.ProjectManagementTask on milestones.MilestoneId equals tasks.MileStoneId
                                  where (milestones.ProjectId == projectId)
                                  select tasks).ToListAsync<ProjectManagementTask>();

                return list.AsQueryable<ProjectManagementTask>();
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }


        }
        /*
        public async Task<IQueryable<ProjectManagementMilestone>> GetMilestones(int projectId)
        {
           
            var list = await base.GetObjectsQueryable(e => e.ProjectId == projectId, "ProjectManagmentMilestone").ToListAsync();

            return list.AsQueryable<ProjectManagementMilestone>();
           
        }
        */
    }
}
