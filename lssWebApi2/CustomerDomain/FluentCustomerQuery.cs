using ERP_Core2.AbstractFactory;
using ERP_Core2.Interfaces;
using ERP_Core2.AccountsReceivableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.CustomerDomain;
using ERP_Core2.CustomerLedgerDomain;
using ERP_Core2.InvoicesDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentCustomerQuery : FluentCustomer, IFluentCustomerQuery
    {
     
        public FluentCustomerQuery()
        {
         
        }
        public IList<AccountReceiveableView> GetAccountReceivables(long customerId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetAccountReceivablesByCustomerId(customerId);
               
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IList<CustomerLedgerView> GetCustomerLedgers(long customerId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetCustomerLedgersByCustomerId(customerId);
     
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }


        public IList<EmailView> GetEmails(long customerId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetEmailsByCustomerId(customerId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public IList<PhoneView> GetPhones(long customerId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetPhonesByCustomerId(customerId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public IList<InvoiceView> GetInvoices(long customerId, long? invoiceId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetInvoicesByCustomerId(customerId, invoiceId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<ScheduleEventView> GetScheduleEvent(long customerId, long serviceId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetScheduleEventsByCustomerId(customerId, serviceId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<LocationAddressView> GetLocationAddress(long customerId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetLocationAddressByCustomerId(customerId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public IList<CustomerClaimView> GetCustomerClaims(long customerId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetCustomerClaimsByCustomerId(customerId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<ContractView> GetContracts(long customerId, long contractId)
        {
            try
            {
                return base.unitOfWork.customerRepository.GetContractsByCustomerId(customerId, contractId);
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
