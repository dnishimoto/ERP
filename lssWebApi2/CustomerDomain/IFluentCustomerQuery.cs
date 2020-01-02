using lssWebApi2.AutoMapper;
using lssWebApi2.CustomerDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentCustomerQuery
{
        Task<Customer> MapToEntity(CustomerView inputObject);
        Task<List<Customer>> MapToEntity(List<CustomerView> inputObjects);
    
        Task<CustomerView> MapToView(Customer inputObject);
        Task<NextNumber> GetNextNumber();
	Task<Customer> GetEntityById(long ? customerId);
	  Task<Customer> GetEntityByNumber(long customerNumber);
	Task<CustomerView> GetViewById(long ? customerId);
	Task<CustomerView> GetViewByNumber(long customerNumber);
}
