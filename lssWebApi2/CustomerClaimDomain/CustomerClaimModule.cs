using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.UDCDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.EmployeeDomain;

namespace lssWebApi2.CustomerClaimDomain
{
    public class CustomerClaimModule : AbstractModule
    {
        public FluentCustomerClaim CustomerClaim = new FluentCustomerClaim();
        public FluentUdc Udc = new FluentUdc();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentEmployee Employee = new FluentEmployee();
    }
}
