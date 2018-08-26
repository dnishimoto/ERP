using ERP_Core2.AbstractFactory;
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

namespace ERP_Core2.FluentAPI
{
    public class FluentCustomerQuery : FluentCustomer, ICustomerQuery
    {
        public IList<AccountReceiveableView> listAccountsReceivableViews;
        public IList<CustomerLedgerView> listCustomerLedgerViews;
        public IList<EmailView> listEmailViews;
        public IList<PhoneView> listPhoneViews;
        public IList<InvoiceView> listInvoiceViews;
        public IList<ScheduleEventView> listScheduleEventViews;
        public IList<LocationAddressView> listLocationAddressViews;
        public IList<CustomerClaimView> listCustomerClaimViews;
        public IList<ContractView> listContractViews;

        public FluentCustomerQuery()
        {
            listAccountsReceivableViews = new List<AccountReceiveableView>();
            listCustomerLedgerViews = new List<CustomerLedgerView>();
            listEmailViews = new List<EmailView>();
            listPhoneViews = new List<PhoneView>();
            listInvoiceViews = new List<InvoiceView>();
            listScheduleEventViews = new List<ScheduleEventView>();
            listLocationAddressViews = new List<LocationAddressView>();
            listCustomerClaimViews = new List<CustomerClaimView>();
            listContractViews = new List<ContractView>();
        }
        public ICustomerQuery WithAccountReceivables(long customerId)
        {
            try
            {
                listAccountsReceivableViews = base.unitOfWork.customerRepository.GetAccountReceivablesByCustomerId(customerId);
                return this as ICustomerQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public ICustomerQuery WithCustomerLedgers(long customerId)
        {
            try
            {
                listCustomerLedgerViews = base.unitOfWork.customerRepository.GetCustomerLedgersByCustomerId(customerId);
                return this as ICustomerQuery;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }


        public ICustomerQuery WithEmails(long customerId)
        {
            try
            {
                listEmailViews = base.unitOfWork.customerRepository.GetEmailsByCustomerId(customerId);
                return this as ICustomerQuery;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public ICustomerQuery WithPhones(long customerId)
        {
            try
            {
                listPhoneViews = base.unitOfWork.customerRepository.GetPhonesByCustomerId(customerId);
                return this as ICustomerQuery;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public ICustomerQuery WithInvoices(long customerId, long? invoiceId)
        {
            try
            {
                listInvoiceViews = base.unitOfWork.customerRepository.GetInvoicesByCustomerId(customerId, invoiceId);
                return this as ICustomerQuery;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public ICustomerQuery WithScheduleEvent(long customerId, long serviceId)
        {
            try
            {
                listScheduleEventViews = base.unitOfWork.customerRepository.GetScheduleEventsByCustomerId(customerId, serviceId);
                return this as ICustomerQuery;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public ICustomerQuery WithLocationAddress(long customerId)
        {
            try
            {
                listLocationAddressViews = base.unitOfWork.customerRepository.GetLocationAddressByCustomerId(customerId);
                return this as ICustomerQuery;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public ICustomerQuery WithCustomerClaims(long customerId)
        {
            try
            {
                listCustomerClaimViews = base.unitOfWork.customerRepository.GetCustomerClaimsByCustomerId(customerId);
                return this as ICustomerQuery;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public ICustomerQuery WithContracts(long customerId, long contractId)
        {
            try
            {
                listContractViews = base.unitOfWork.customerRepository.GetContractsByCustomerId(customerId, contractId);
                return this as ICustomerQuery;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
