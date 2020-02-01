using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.UDCDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;

namespace lssWebApi2.SupervisorDomain
{
    public class SupervisorModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentSupervisor Supervisor;
        public FluentAddressBook AddressBook;
        public FluentUdc Udc;

        public SupervisorModule()
        {
            unitOfWork = new UnitOfWork();
            Supervisor = new FluentSupervisor(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
        }
    }
}
