using lssWebApi2.AbstractFactory;
using lssWebApi2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.UDCDomain;
using lssWebApi2.Services;

namespace lssWebApi2.LocationAddressDomain
{
    public class LocationAddressModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentLocationAddress LocationAddress;
        public FluentAddressBook AddressBook;
        public FluentUdc Udc;
        public LocationAddressModule()
        {
            unitOfWork = new UnitOfWork();
            LocationAddress = new FluentLocationAddress(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
        }

    }
}
