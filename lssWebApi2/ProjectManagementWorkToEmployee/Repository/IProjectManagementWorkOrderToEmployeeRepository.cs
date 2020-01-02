

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain
{
public interface IProjectManagementWorkOrderToEmployeeRepository
    {
        Task<ProjectManagementWorkOrderToEmployee> GetEntityById(long ? projectManagementWorkOrderToEmployeeId);
	    Task<ProjectManagementWorkOrderToEmployee> FindEntityByExpression(Expression<Func<ProjectManagementWorkOrderToEmployee, bool>> predicate);
		Task<IList<ProjectManagementWorkOrderToEmployee>> FindEntitiesByExpression(Expression<Func<ProjectManagementWorkOrderToEmployee, bool>> predicate);
    }
}
