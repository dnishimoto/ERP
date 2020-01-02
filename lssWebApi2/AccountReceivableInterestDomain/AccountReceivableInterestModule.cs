using lssWebApi2.AbstractFactory;
using lssWebApi2.AccountReceivableInterestDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.CustomerDomain;

namespace lssWebApi2.AccountReceivableInterestDomain
{
    public class AccountReceivableInterestModule : AbstractModule
    {
        public FluentAccountReceivableInterest AccountReceivableInterest = new FluentAccountReceivableInterest();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentAccountReceivable AccountReceivable = new FluentAccountReceivable();
    }
}
