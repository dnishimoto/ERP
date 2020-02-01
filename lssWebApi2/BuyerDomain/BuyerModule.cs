using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.BuyerDomain
{
    public class BuyerModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentBuyer Buyer;
        public FluentAddressBook AddressBook;
        public BuyerModule()
        {
            unitOfWork = new UnitOfWork();
            Buyer = new FluentBuyer(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
