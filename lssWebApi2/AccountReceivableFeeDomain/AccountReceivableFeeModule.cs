using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lssWebApi2.CustomerDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.AccountsReceivableDomain;

namespace lssWebApi2.AccountReceivableDomain
{
    public class AccountReceivableFeeModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentAccountReceivableFee AccountReceivableFee;
        public FluentCustomer Customer;
        public FluentAddressBook AddressBook;
        public FluentAccountReceivable AccountReceivable;

        public AccountReceivableFeeModule()
        {
            unitOfWork = new UnitOfWork();
            AccountReceivableFee = new FluentAccountReceivableFee(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            AccountReceivable = new FluentAccountReceivable(unitOfWork);
        }
    }
}
