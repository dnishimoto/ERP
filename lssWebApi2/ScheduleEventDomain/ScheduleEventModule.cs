using lssWebApi2.AbstractFactory;
using lssWebApi2.ScheduleEventDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ServiceInformationDomain;

namespace lssWebApi2.ScheduleEventDomain
{
    public class ScheduleEventModule : AbstractModule
    {
        public FluentScheduleEvent ScheduleEvent = new FluentScheduleEvent();
        public FluentEmployee Employee = new FluentEmployee();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentServiceInformation ServiceInformation = new FluentServiceInformation();
    }
}
