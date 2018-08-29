using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
using ERP_Core2.Interfaces;
using MillenniumERP.AccountsReceivableDomain;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.CustomerDomain;
using MillenniumERP.CustomerLedgerDomain;
using MillenniumERP.InvoicesDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.CustomerDomain
{


    public class CustomerModule : AbstractModule
    {

        public FluentCustomer Customer = new FluentCustomer();

        public IList<AccountReceiveableView> AccountReceivableViews { get { return Customer._query.listAccountsReceivableViews; } }
        public IList<CustomerLedgerView> CustomerLedgerViews { get { return Customer._query.listCustomerLedgerViews; } }
        public IList<EmailView> EmailViews { get { return Customer._query.listEmailViews; } }
        public IList<PhoneView> PhoneViews { get { return Customer._query.listPhoneViews; } }
        public IList<InvoiceView> InvoiceViews { get { return Customer._query.listInvoiceViews; } }
        public IList<ScheduleEventView> ScheduleEventViews { get { return Customer._query.listScheduleEventViews; } }
        public IList<LocationAddressView> LocationAddressViews { get { return Customer._query.listLocationAddressViews; } }
        public IList<CustomerClaimView> CustomerClaimViews { get { return Customer._query.listCustomerClaimViews; } }
        public IList<ContractView> ContractViews { get { return Customer._query.listContractViews; } }
    }
}
