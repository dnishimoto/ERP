using lssWebApi2.AbstractFactory;
using lssWebApi2.SupervisorDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.UDCDomain;

namespace lssWebApi2.SupervisorDomain
{
    public class SupervisorModule : AbstractModule
    {
        public FluentSupervisor Supervisor = new FluentSupervisor();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentUdc Udc = new FluentUdc();
    }
}
