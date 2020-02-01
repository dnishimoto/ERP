using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ChartOfAccountsDomain;
using lssWebApi2.Services;
using lssWebApi2.InvoiceDomain;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.CustomerLedgerDomain
{
    public class CustomerLedgerModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentCustomerLedger CustomerLedger;
        public FluentCustomer Customer;
        public FluentInvoice Invoice;
        public FluentAccountReceivable AccountReceivable;
        public FluentGeneralLedger GeneralLedger;
        public FluentAddressBook AddressBook;
        public FluentChartOfAccount ChartOfAccount;
        public CustomerLedgerModule()
        {
            unitOfWork = new UnitOfWork();
            CustomerLedger = new FluentCustomerLedger(unitOfWork);
            Customer = new FluentCustomer(unitOfWork);
            Invoice = new FluentInvoice(unitOfWork);
            AccountReceivable = new FluentAccountReceivable(unitOfWork);
            GeneralLedger = new FluentGeneralLedger(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
        }
    }
}
