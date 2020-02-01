using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.CustomerLedgerDomain;
using lssWebApi2.CustomerClaimDomain;
using lssWebApi2.ContractDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.LocationAddressDomain;
using lssWebApi2.Services;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.EmailDomain;

namespace lssWebApi2.CustomerDomain
{
    public class CustomerModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentCustomer Customer;
        public FluentCustomerLedger CustomerLedger;
        public FluentAccountReceivable AccountsReceivable;
        public FluentCustomerClaim CustomerClaim;
        public FluentContract Contract;
        public FluentEmail Email;
        public FluentPhone Phone;
        public FluentLocationAddress LocationAddress;
        public FluentScheduleEvent ScheduleEvent;
        public FluentInvoice Invoice;
        public FluentAddressBook AddressBook;
        public CustomerModule()
        {
            unitOfWork = new UnitOfWork();
            Customer = new FluentCustomer(unitOfWork);
            CustomerLedger = new FluentCustomerLedger(unitOfWork);
            AccountsReceivable = new FluentAccountReceivable(unitOfWork);
            CustomerClaim = new FluentCustomerClaim(unitOfWork);
            Contract = new FluentContract(unitOfWork);
            Email = new FluentEmail(unitOfWork);
            Phone = new FluentPhone(unitOfWork);
            LocationAddress = new FluentLocationAddress(unitOfWork);
            ScheduleEvent = new FluentScheduleEvent(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
        }
    }
}
