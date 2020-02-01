using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using lssWebApi2.CustomerDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.AccountsReceivableDomain;

namespace lssWebApi2.AccountReceivableInterestDomain
{
    public class AccountReceivableInterestModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentAccountReceivableInterest AccountReceivableInterest;
        public FluentCustomer Customer;
        public FluentAddressBook AddressBook;
        public FluentAccountReceivable AccountReceivable;
        public AccountReceivableInterestModule()
        {
            unitOfWork = new UnitOfWork();
            AccountReceivableInterest = new FluentAccountReceivableInterest(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            AccountReceivable = new FluentAccountReceivable(unitOfWork);
        }
    }
}
