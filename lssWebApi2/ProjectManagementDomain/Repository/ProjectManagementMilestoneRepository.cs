using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ProjectManagementDomain
{
    public class ProjectManagementTaskRepository : Repository<ProjectManagementTask>
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementTaskRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IQueryable<ProjectManagementTask>> GetEmployeeByTaskId(int taskId)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.TaskId == taskId, "").ToListAsync();

                return list.AsQueryable<ProjectManagementTask>();
            }
            catch(Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
