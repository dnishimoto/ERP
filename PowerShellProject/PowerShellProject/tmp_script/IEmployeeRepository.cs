

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP_Core2.AddressBookDomain
{
public interface IEmployeeRepository
    {
        Task<Employee> GetEntityById(long _employeeId);
		Task<List<Employee>> GetObjectsQueryable(Expression<Func<Employee, bool>> predicate, string include);
   
    }
}
