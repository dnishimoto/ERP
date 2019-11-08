using ERP_Core2.AddressBookDomain;
using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentEmployeeQuery
{
    Task<Employee> MapToEntity(EmployeeView inputObject);
    Task<List<Employee>> MapToEntity(List<EmployeeView> inputObjects);
    Task<EmployeeView> MapToView(Employee inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Employee> GetEntityById(long employeeId);
    Task<Employee> GetEntityByNumber(long employeeNumber);
    Task<EmployeeView> GetViewById(long employeeId);
    Task<EmployeeView> GetViewByNumber(long employeeNumber);
    Task<List<EmployeeView>> GetViewsBySupervisorId(long supervisorId);
}
