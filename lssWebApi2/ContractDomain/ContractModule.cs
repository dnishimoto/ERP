using lssWebApi2.AbstractFactory;
using lssWebApi2.ContractDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.CustomerDomain;
using lssWebApi2.UDCDomain;

namespace lssWebApi2.ContractDomain
{
    public class ContractModule : AbstractModule
    {
        public FluentContract Contract = new FluentContract();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentUdc Udc = new FluentUdc();
        public FluentAddressBook AddressBook = new FluentAddressBook();
    }
}
