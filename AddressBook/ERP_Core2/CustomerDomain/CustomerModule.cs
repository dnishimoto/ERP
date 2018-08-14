using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
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
        UnitOfWork unitOfWork = new UnitOfWork();

        public IList<CustomerLedgerView> GetCustomerLedgersByCustomerId(long customerId)
        {
            try
            {
                IList<CustomerLedgerView> list = unitOfWork.customerRepository.GetCustomerLedgersByCustomerId(customerId);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<bool> CreateCustomerAccount(CustomerView customerView)
        {
            try
            {
                customerView.AddressId = await unitOfWork.addressBookRepository.CreateAddressBook(customerView);

                if (customerView.AddressId > 0)
                {
                    EmailView emailView = new EmailView();
                    emailView.AddressId = customerView.AddressId;
                    emailView.EmailText = customerView.AccountEmail.EmailText;
                    emailView.LoginEmail = customerView.AccountEmail.LoginEmail;
                    emailView.Password = customerView.AccountEmail.Password;

                    bool result2 = await unitOfWork.emailRepository.CreateEmail(emailView);
                    if (result2) { unitOfWork.CommitChanges(); }

                }
                AddressBook lookupAddressBook = await unitOfWork.addressBookRepository.GetAddressBookByCustomerView(customerView);

                bool result3 = await unitOfWork.customerRepository.CreateCustomer(customerView);
                if (result3)
                {
                    unitOfWork.CommitChanges();
                }


                customerView.AddressId = lookupAddressBook.AddressId;

                bool result4 = await unitOfWork.locationAddressRepository.CreateLocationUsingCustomer(customerView);
                if (result4)
                {
                    unitOfWork.CommitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IList<AccountReceiveableView> GetAccountReceivablesByCustomerId(long customerId)
        {
            try
            {
                IList<AccountReceiveableView> list = unitOfWork.customerRepository.GetAccountReceivablesByCustomerId(customerId);
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public IList<EmailView> GetEmailsByCustomerId(long customerId)
        {
            try
            {
                IList<EmailView> list = unitOfWork.customerRepository.GetEmailsByCustomerId(customerId);
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public IList<PhoneView> GetPhonesByCustomerId(long customerId)
        {
            try
            {
                IList<PhoneView> list = unitOfWork.customerRepository.GetPhonesByCustomerId(customerId);
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public IList<InvoiceView> GetInvoicesByCustomerId(long customerId, long? invoiceId)
        {
            try
            {
                IList<InvoiceView> list = unitOfWork.customerRepository.GetInvoicesByCustomerId(customerId, invoiceId);
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<ScheduleEventView> GetScheduleEventsByCustomerId(long customerId, long serviceId)
        {
            try { 
            IList<ScheduleEventView> list = unitOfWork.customerRepository.GetScheduleEventsByCustomerId(customerId, serviceId);
            return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<LocationAddressView> GetLocationAddressByCustomerId(long customerId)
        {
            try
            {
                IList<LocationAddressView> list = unitOfWork.customerRepository.GetLocationAddressByCustomerId(customerId);
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }

        public  IList<CustomerClaimView> GetCustomerClaimsByCustomerId(long customerId)
        {
            try
            {
                IList<CustomerClaimView> list = unitOfWork.customerRepository.GetCustomerClaimsByCustomerId(customerId);
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public IList<ContractView> GetContractsByCustomerId(long customerId,long contractId)
        {
            try
            {
                IList<ContractView> list = unitOfWork.customerRepository.GetContractsByCustomerId(customerId, contractId);
                return list;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
