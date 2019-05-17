using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.InvoicesDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.CustomerDomain.Repository
{
    public interface ICustomerRepository
    {
        Task<CreateProcessStatus> CreateCustomer(CustomerView customerView);
        IList<CustomerLedgerView> GetCustomerLedgersByCustomerId(long customerId);
        IList<InvoiceView> GetInvoicesByCustomerId(long customerId, long? invoiceId = null);
        IList<CustomerClaimView> GetCustomerClaimsByCustomerId(long customerId);
        IList<ScheduleEventView> GetScheduleEventsByCustomerId(long customerId, long? serviceId = null);
        IList<ContractView> GetContractsByCustomerId(long customerId, long? contractId = null);
        IList<LocationAddressView> GetLocationAddressByCustomerId(long customerId);
        IList<PhoneView> GetPhonesByCustomerId(long customerId);
        IList<EmailView> GetEmailsByCustomerId(long customerId);
        IList<AccountReceiveableView> GetAccountReceivablesByCustomerId(long customerId);
    }
}
