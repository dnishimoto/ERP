using lssWebApi2.AbstractFactory;
using lssWebApi2.CarrierDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.UDCDomain;

namespace lssWebApi2.CarrierDomain
{
    public class CarrierModule : AbstractModule
    {
        public FluentCarrier Carrier = new FluentCarrier();
        public FluentUdc Udc = new FluentUdc();
        public FluentAddressBook AddressBook = new FluentAddressBook();
    }
}
