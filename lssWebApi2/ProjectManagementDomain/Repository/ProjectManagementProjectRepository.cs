using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ProjectManagementDomain
{

    public class ProjectManagementProjectRepository: Repository<ProjectManagementProject>
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementProjectRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<ProjectManagementProject> GetProjectByNumber(long projectNumber)
        {
            try
            {
                var query = await(from project in _dbContext.ProjectManagementProject

                                 where (project.ProjectNumber==projectNumber)
                                 select project).FirstOrDefaultAsync<ProjectManagementProject>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<IQueryable<ProjectManagementWorkOrder>> GetWorkOrdersByProjectId(long projectId)
        {
            try
            {
                var list = await (from workorders in _dbContext.ProjectManagementWorkOrder

                                  where (workorders.ProjectId == projectId)
                                  select workorders).ToListAsync<ProjectManagementWorkOrder>();

                return list.AsQueryable<ProjectManagementWorkOrder>();
            }
            catch (Exception ex) {
                throw new Exception(GetMyMethodName(), ex);
            }
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
      
    }
}
