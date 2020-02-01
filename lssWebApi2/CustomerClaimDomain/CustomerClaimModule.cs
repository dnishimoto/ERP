using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.UDCDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.CustomerClaimDomain
{
    public class CustomerClaimModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentCustomerClaim CustomerClaim;
        public FluentUdc Udc;
        public FluentCustomer Customer;
        public FluentAddressBook AddressBook;
        public FluentEmployee Employee;

        public CustomerClaimModule()
        {
            unitOfWork = new UnitOfWork();
            CustomerClaim = new FluentCustomerClaim(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
        }
    }
}
