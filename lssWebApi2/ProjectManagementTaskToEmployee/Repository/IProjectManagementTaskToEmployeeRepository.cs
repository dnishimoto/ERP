

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.ProjectManagementTaskToEmployeeDomain
{
public interface IProjectManagementTaskToEmployeeRepository
    {
        Task<ProjectManagementTaskToEmployee> GetEntityById(long ? projectManagementTaskToEmployeeId);
	    Task<ProjectManagementTaskToEmployee> FindEntityByExpression(Expression<Func<ProjectManagementTaskToEmployee, bool>> predicate);
		Task<IList<ProjectManagementTaskToEmployee>> FindEntitiesByExpression(Expression<Func<ProjectManagementTaskToEmployee, bool>> predicate);
    }
}
