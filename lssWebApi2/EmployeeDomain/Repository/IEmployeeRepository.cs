

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

using System;

namespace lssWebApi2.EmployeeDomain
{
public interface IEmployeeRepository
    {
        Task<Employee> GetEntityById(long ? employeeId);
	    Task<IList<Employee>> GetEntitiesBySupervisorId(long supervisorId);
        Task<IList<Employee>> GetEmployeeByWorkOrderId(long? workOrderId);
        Task<IList<Employee>> GetEmployeeByTaskId(int? taskId);

    }
}
