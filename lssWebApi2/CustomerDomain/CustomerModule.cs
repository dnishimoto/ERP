using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.CustomerClaimDomain;
using lssWebApi2.ContractDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.LocationAddressDomain;

namespace lssWebApi2.CustomerDomain
{
    public class CustomerModule : AbstractModule
    {
        public FluentCustomer Customer = new FluentCustomer();
        public FluentCustomerLedger CustomerLedger = new FluentCustomerLedger();
        public FluentAccountReceivable AccountsReceivable = new FluentAccountReceivable();
        public FluentCustomerClaim CustomerClaim = new FluentCustomerClaim();
        public FluentContract Contract = new FluentContract();
        public FluentEmail Email = new FluentEmail();
        public FluentPhone Phone = new FluentPhone();
        public FluentLocationAddress LocationAddress = new FluentLocationAddress();
        public FluentScheduleEvent ScheduleEvent = new FluentScheduleEvent();
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentAddressBook AddressBook = new FluentAddressBook();
    }
}
