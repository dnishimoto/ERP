using lssWebApi2.AbstractFactory;
using lssWebApi2.ScheduleEventDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ServiceInformationDomain;
using lssWebApi2.Services;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.ScheduleEventDomain
{
    public class ScheduleEventModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentScheduleEvent ScheduleEvent;
        public FluentEmployee Employee;
        public FluentAddressBook AddressBook;
        public FluentCustomer Customer;
        public FluentServiceInformation ServiceInformation;

        public ScheduleEventModule()
        {
            unitOfWork = new UnitOfWork();
            ScheduleEvent = new FluentScheduleEvent(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            ServiceInformation = new FluentServiceInformation(unitOfWork);
        }
    }
}
