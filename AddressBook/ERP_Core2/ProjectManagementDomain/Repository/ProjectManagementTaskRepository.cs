using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.EntityFramework;
using MillenniumERP.Services;

namespace MillenniumERP.ProjectManagementDomain
{
    public class ProjectManagementMilestoneRepository : Repository<ProjectManagementMilestone>
    {
        Entities _dbContext;
        public ProjectManagementMilestoneRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }

        public async Task<IQueryable<ProjectManagementMilestone>> GetTasksByMilestoneId(int milestoneId)
        {

            var list = await base.GetObjectsAsync(e => e.MilestoneId == milestoneId, "ProjectManagementTasks").ToListAsync();

            return list.AsQueryable<ProjectManagementMilestone>();

        }
    }
}
