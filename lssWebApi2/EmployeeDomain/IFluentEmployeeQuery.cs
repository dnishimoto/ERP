using lssWebApi2.AddressBookDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.EmployeeDomain
{
    public interface IFluentEmployeeQuery
    {
        Task<Employee> MapToEntity(EmployeeView inputObject);
        Task<IList<Employee>> MapToEntity(IList<EmployeeView> inputObjects);
        Task<EmployeeView> MapToView(Employee inputObject);
        Task<NextNumber> GetNextNumber();
        Task<Employee> GetEntityById(long? employeeId);
        Task<Employee> GetEntityByNumber(long employeeNumber);
        Task<EmployeeView> GetViewById(long? employeeId);
        Task<EmployeeView> GetViewByNumber(long employeeNumber);
        Task<List<EmployeeView>> GetViewsBySupervisorId(long supervisorId);
        Task<IEnumerable<EmployeeView>> GetEntitiesByWorkOrderId(long workOrderId);

    }
}
