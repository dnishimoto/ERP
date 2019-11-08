

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.AddressBookDomain
{ 

public interface IFluentEmployee
    {
        IFluentEmployeeQuery Query();
        IFluentEmployee Apply();
        IFluentEmployee AddEmployee(Employee employee);
        IFluentEmployee UpdateEmployee(Employee employee);
        IFluentEmployee DeleteEmployee(Employee employee);
     	IFluentEmployee UpdateEmployees(List<Employee> newObjects);
        IFluentEmployee AddEmployees(List<Employee> newObjects);
        IFluentEmployee DeleteEmployees(List<Employee> deleteObjects);
    }
}
