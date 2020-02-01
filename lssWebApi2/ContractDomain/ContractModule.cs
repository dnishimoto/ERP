using lssWebApi2.AbstractFactory;
using lssWebApi2.ContractDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lssWebApi2.CustomerDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.ContractDomain
{
    public class ContractModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentContract Contract;
        public FluentCustomer Customer;
        public FluentUdc Udc;
        public FluentAddressBook AddressBook;
        public ContractModule()
        {
            unitOfWork = new UnitOfWork();
            Contract = new FluentContract(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
