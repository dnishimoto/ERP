using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ICustomerQuery

    {
        IList<AccountReceiveableView> GetAccountReceivables(long customerId);
        IList<CustomerLedgerView> GetCustomerLedgers(long customerId);
        IList<EmailView> GetEmails(long customerId);
        IList<PhoneView> GetPhones(long customerId);
        IList<InvoiceView> GetInvoices(long customerId, long? invoiceId);
        IList<ScheduleEventView> GetScheduleEvent(long customerId, long serviceId);
        IList<LocationAddressView> GetLocationAddress(long customerId);
        IList<CustomerClaimView> GetCustomerClaims(long customerId);
        IList<ContractView> GetContracts(long customerId, long contractId);

    }
}
