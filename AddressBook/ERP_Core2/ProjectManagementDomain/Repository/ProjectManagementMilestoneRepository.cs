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
    public class ProjectManagementTaskRepository : Repository<ProjectManagementTask>
    {
        Entities _dbContext;
        public ProjectManagementTaskRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
        }

        public async Task<IQueryable<ProjectManagementTask>> GetEmployeeByTaskId(int taskId)
        {
            try
            {
                var list = await base.GetObjectsAsync(e => e.TaskId == taskId, "").ToListAsync();

                return list.AsQueryable<ProjectManagementTask>();
            }
            catch(Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
