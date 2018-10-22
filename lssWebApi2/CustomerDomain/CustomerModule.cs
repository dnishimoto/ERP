using ERP_Core2.AbstractFactory;
using ERP_Core2.FluentAPI;
using System;

namespace ERP_Core2.CustomerDomain
{


    public class CustomerModule 
    {

        public FluentCustomer Customer = new FluentCustomer();

        public bool CreateCustomer(CustomerView customerView)
        {
            try
            {
                Customer

                       .CreateAddressBook(customerView)
                       .Apply()
                       .CreateCustomerEmail(customerView)
                       .Apply()
                       .CreateCustomer(customerView)
                       .Apply()
                       .CreateCustomerLocationAddress(customerView)
                       .Apply()
                       ;
                return true;
            }
            catch (Exception ex) { throw new Exception("CreateCustomer", ex); }

        }
      }
}
