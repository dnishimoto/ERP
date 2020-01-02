   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{
    public partial class ProjectManagementTaskToEmployeeView
    {
        public long TaskToEmployeeId { get; set; }
        public long EmployeeId { get; set; }
        public long TaskId { get; set; }
        public long TaskToEmployeeNumber { get; set; }

        public string EmployeeName { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string MilestoneName { get; set; }
        public string ProjectName { get; set; }

    }

    public class ProjectManagementTaskToEmployeeRepository: Repository<ProjectManagementTaskToEmployee>, IProjectManagementTaskToEmployeeRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementTaskToEmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<ProjectManagementTaskToEmployee>GetEntityById(long ? projectManagementTaskToEmployeeId)
        {
			try{
            return await _dbContext.FindAsync<ProjectManagementTaskToEmployee>(projectManagementTaskToEmployeeId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<ProjectManagementTaskToEmployee> GetEntityByNumber(long projectManagementTaskToEmployeeNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.ProjectManagementTaskToEmployee
                               where detail.TaskToEmployeeNumber == projectManagementTaskToEmployeeNumber
                               select detail).FirstOrDefaultAsync<ProjectManagementTaskToEmployee>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<ProjectManagementTaskToEmployee> FindEntityByExpression(Expression<Func<ProjectManagementTaskToEmployee, bool>> predicate)
        {
            IQueryable<ProjectManagementTaskToEmployee> result = _dbContext.Set<ProjectManagementTaskToEmployee>().Where(predicate);

            return await result.FirstOrDefaultAsync<ProjectManagementTaskToEmployee>();
        }
		  public async Task<IList<ProjectManagementTaskToEmployee>> FindEntitiesByExpression(Expression<Func<ProjectManagementTaskToEmployee, bool>> predicate)
        {
            IQueryable<ProjectManagementTaskToEmployee> result = _dbContext.Set<ProjectManagementTaskToEmployee>().Where(predicate);

            return await result.ToListAsync<ProjectManagementTaskToEmployee>();
        }
		
  }
}
