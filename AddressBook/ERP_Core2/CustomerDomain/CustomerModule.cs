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

  
   
    public interface ICustomerModule
    {
        ICustomer Customer();
    }
    public class CustomerModule : AbstractModule, ICustomerModule
    {
        private FluentCustomer _Customer;
        public ICustomer Customer()
        {
            _Customer = new FluentCustomer();
            return _Customer as ICustomer;
        }

        public IList<AccountReceiveableView> AccountReceivableViews { get { return _Customer._query.listAccountsReceivableViews; } }
        public IList<CustomerLedgerView> CustomerLedgerViews { get { return _Customer._query.listCustomerLedgerViews; } }
        public IList<EmailView> EmailViews { get { return _Customer._query.listEmailViews; } }
        public IList<PhoneView> PhoneViews { get { return _Customer._query.listPhoneViews; } }
        public IList<InvoiceView> InvoiceViews { get { return _Customer._query.listInvoiceViews; } }
        public IList<ScheduleEventView> ScheduleEventViews { get { return _Customer._query.listScheduleEventViews; } }
        public IList<LocationAddressView> LocationAddressViews { get { return _Customer._query.listLocationAddressViews; } }
        public IList<CustomerClaimView> CustomerClaimViews { get { return _Customer._query.listCustomerClaimViews; } }
        public IList<ContractView> ContractViews { get { return _Customer._query.listContractViews; } }
    }
}
