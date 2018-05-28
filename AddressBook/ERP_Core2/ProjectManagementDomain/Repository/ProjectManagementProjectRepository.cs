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

    public class ProjectManagementProjectRepository: Repository<ProjectManagementMilestone>
    {
        Entities _dbContext;
        public ProjectManagementProjectRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }
        public async Task<IQueryable<ProjectManagementMilestone>> GetMilestones(int projectId)
        {

            var list = await base.GetObjectsAsync(e => e.ProjectId == projectId, "ProjectManagementMilestone").ToListAsync();

            return list.AsQueryable<ProjectManagementMilestone>();

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
