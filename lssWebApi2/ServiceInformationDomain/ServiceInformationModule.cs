using lssWebApi2.AbstractFactory;
using lssWebApi2.ServiceInformationDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.LocationAddressDomain;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ContractDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.ServiceInformationInvoiceDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;

namespace lssWebApi2.ServiceInformationDomain
{
    public class ServiceInformationModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentServiceInformation ServiceInformation;
        public FluentServiceInformationInvoice ServiceInformationInvoice;
        public FluentLocationAddress LocationAddress;
        public FluentAddressBook AddressBook;
        public FluentCustomer Customer;
        public FluentContract Contract;
        public FluentUdc Udc;
        public FluentScheduleEvent ScheduleEvent;

        public ServiceInformationModule()
        {
            unitOfWork = new UnitOfWork();
            ServiceInformation = new FluentServiceInformation(unitOfWork);
            ServiceInformationInvoice = new FluentServiceInformationInvoice(unitOfWork);
            LocationAddress = new FluentLocationAddress(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            Contract = new FluentContract(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            ScheduleEvent = new FluentScheduleEvent(unitOfWork);
        }

    }
}
