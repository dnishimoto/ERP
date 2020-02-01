using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AddressBookDomain
{
    public class PhoneModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentPhone Phone;
        public FluentAddressBook AddressBook;

        public PhoneModule()
        {
            unitOfWork = new UnitOfWork();
            Phone = new FluentPhone(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
