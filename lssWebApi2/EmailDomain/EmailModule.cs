using lssWebApi2.AbstractFactory;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace lssWebApi2.EmailDomain
{
    public class EmailModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentEmail Email;
        public FluentAddressBook AddressBook;

        public EmailModule()
        {
            unitOfWork = new UnitOfWork();
            Email = new FluentEmail(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
