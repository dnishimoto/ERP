using lssWebApi2.AbstractFactory;
using lssWebApi2.CustomerLedgerDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.CustomerDomain;
using lssWebApi2.ChartOfAccountsDomain;

namespace lssWebApi2.CustomerLedgerDomain
{
    public class CustomerLedgerModule : AbstractModule
    {
        public FluentCustomerLedger CustomerLedger = new FluentCustomerLedger();
        public FluentCustomer Customer = new FluentCustomer();
        public FluentInvoice Invoice = new FluentInvoice();
        public FluentAccountReceivable AccountReceivable = new FluentAccountReceivable();
        public FluentGeneralLedger GeneralLedger = new FluentGeneralLedger();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
    }
}
