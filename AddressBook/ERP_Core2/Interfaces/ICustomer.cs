using MillenniumERP.CustomerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ICustomer
    {
        ICustomer CreateAddressBook(CustomerView customerView);
        ICustomer CreateCustomerLocationAddress(CustomerView customerView);
        ICustomer CreateCustomer(CustomerView customerView);
        ICustomer CreateCustomerEmail(CustomerView customerView);
        ICustomer Apply();
        ICustomerQuery Query();
    }
}
