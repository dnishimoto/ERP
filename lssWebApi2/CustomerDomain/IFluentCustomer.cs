

using lssWebApi2.Interfaces;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.CustomerDomain
{ 

public interface IFluentCustomer
    {
        IFluentCustomerQuery Query();
        IFluentCustomer Apply();
        IFluentCustomer AddCustomer(Customer customer);
        IFluentCustomer UpdateCustomer(Customer customer);
        IFluentCustomer DeleteCustomer(Customer customer);
     	IFluentCustomer UpdateCustomers(List<Customer> newObjects);
        IFluentCustomer AddCustomers(List<Customer> newObjects);
        IFluentCustomer DeleteCustomers(List<Customer> deleteObjects);
        IFluentCustomer CreateCustomerByView(CustomerView view);
    }
}
