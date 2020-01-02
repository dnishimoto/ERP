

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{
    public class ProjectManagementWorkOrderToEmployeeView
    {
        public long WorkOrderToEmployeeId { get; set; }
        public long EmployeeId { get; set; }
        public long WorkOrderId { get; set; }
        public long WorkOrderToEmployeeNumber { get; set; }

        public string EmployeeName { get; set; }
        public string WorkOrderDescription { get; set; }
        public string WorkOrderLocation { get; set; }
        public DateTime? WorkOrderStartDate { get; set; }
        public DateTime? WorkOrderEndDate { get; set; }
        public string WorkOrderInstructions { get; set; }


    }

    public class ProjectManagementWorkOrderToEmployeeRepository : Repository<ProjectManagementWorkOrderToEmployee>, IProjectManagementWorkOrderToEmployeeRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementWorkOrderToEmployeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }


        public async Task<ProjectManagementWorkOrderToEmployee> GetEntityById(long? projectManagementWorkOrderToEmployeeId)
        {
            try
            {
                return await _dbContext.FindAsync<ProjectManagementWorkOrderToEmployee>(projectManagementWorkOrderToEmployeeId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<ProjectManagementWorkOrderToEmployee> GetEntityByNumber(long projectManagementWorkOrderToEmployeeNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.ProjectManagementWorkOrderToEmployee
                                   where detail.WorkOrderToEmployeeNumber == projectManagementWorkOrderToEmployeeNumber
                                   select detail).FirstOrDefaultAsync<ProjectManagementWorkOrderToEmployee>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<ProjectManagementWorkOrderToEmployee> FindEntityByExpression(Expression<Func<ProjectManagementWorkOrderToEmployee, bool>> predicate)
        {
            IQueryable<ProjectManagementWorkOrderToEmployee> result = _dbContext.Set<ProjectManagementWorkOrderToEmployee>().Where(predicate);

            return await result.FirstOrDefaultAsync<ProjectManagementWorkOrderToEmployee>();
        }
        public async Task<IList<ProjectManagementWorkOrderToEmployee>> FindEntitiesByExpression(Expression<Func<ProjectManagementWorkOrderToEmployee, bool>> predicate)
        {
            IQueryable<ProjectManagementWorkOrderToEmployee> result = _dbContext.Set<ProjectManagementWorkOrderToEmployee>().Where(predicate);

            return await result.ToListAsync<ProjectManagementWorkOrderToEmployee>();
        }

    }
}
