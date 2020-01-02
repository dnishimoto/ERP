using lssWebApi2.AbstractFactory;
using lssWebApi2.BuyerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.BuyerDomain
{
    public class BuyerModule : AbstractModule
    {
        public FluentBuyer Buyer = new FluentBuyer();
        public FluentAddressBook AddressBook = new FluentAddressBook();
    }
}
