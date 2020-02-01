using lssWebApi2.AbstractFactory;
using lssWebApi2.CarrierDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.UDCDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.CarrierDomain
{
    public class CarrierModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentCarrier Carrier;
        public FluentUdc Udc;
        public FluentAddressBook AddressBook;
        public CarrierModule()
        {
            unitOfWork = new UnitOfWork();
            Carrier = new FluentCarrier(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
