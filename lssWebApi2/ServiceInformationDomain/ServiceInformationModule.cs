using lssWebApi2.AbstractFactory;
using lssWebApi2.ServiceInformationDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.LocationAddressDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ContractDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.ServiceInformationInvoiceDomain;

namespace lssWebApi2.ServiceInformationDomain
{
    public class ServiceInformationModule : AbstractModule
    {
        public FluentServiceInformation ServiceInformation = new FluentServiceInformation();
        public FluentServiceInformationInvoice ServiceInformationInvoice = new FluentServiceInformationInvoice();
        public FluentLocationAddress LocationAddress = new FluentLocationAddress();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentContract Contract = new FluentContract();
        public FluentUdc Udc = new FluentUdc();
        public FluentScheduleEvent ScheduleEvent = new FluentScheduleEvent();

    }
}
