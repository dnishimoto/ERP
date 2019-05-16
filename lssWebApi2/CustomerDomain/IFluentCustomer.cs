using ERP_Core2.CustomerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentCustomer
    {
        IFluentCustomer CreateAddressBook(CustomerView customerView);
        IFluentCustomer CreateCustomerLocationAddress(CustomerView customerView);
        IFluentCustomer CreateCustomer(CustomerView customerView);
        IFluentCustomer CreateCustomerEmail(CustomerView customerView);
        IFluentCustomer Apply();
        IFluentCustomerQuery Query();
    }
}
