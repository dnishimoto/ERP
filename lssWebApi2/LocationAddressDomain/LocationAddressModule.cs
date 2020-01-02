using lssWebApi2.AbstractFactory;
using lssWebApi2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.UDCDomain;

namespace lssWebApi2.LocationAddressDomain
{
    public class LocationAddressModule : AbstractModule
    {
        public FluentLocationAddress LocationAddress = new FluentLocationAddress();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentUdc Udc = new FluentUdc();
    }
}
