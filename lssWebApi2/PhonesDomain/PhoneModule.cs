using lssWebApi2.AbstractFactory;
using lssWebApi2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.AddressBookDomain
{
    public class PhoneModule : AbstractModule
    {
        public FluentPhone Phone = new FluentPhone();
        public FluentAddressBook AddressBook = new FluentAddressBook();
    }
}
