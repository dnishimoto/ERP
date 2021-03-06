﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.EntityFramework;
using ERP_Core2.Services;

namespace ERP_Core2.ProjectManagementDomain
{
    public class ProjectManagementMilestoneRepository : Repository<ProjectManagementMilestone>
    {
        Entities _dbContext;
        public ProjectManagementMilestoneRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }

        public async Task<IQueryable<ProjectManagementMilestone>> GetTasksByMilestoneId(long milestoneId)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.MilestoneId == milestoneId, "ProjectManagementTasks").ToListAsync();

                return list.AsQueryable<ProjectManagementMilestone>();
          
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
