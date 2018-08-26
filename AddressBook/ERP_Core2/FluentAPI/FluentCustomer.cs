using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Interfaces;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Interfaces;
using MillenniumERP.CustomerDomain;
using ERP_Core2.EntityFramework;
using MillenniumERP.AddressBookDomain;

namespace ERP_Core2.FluentAPI
{
    public class FluentCustomer : AbstractModule, ICustomer
    {
        public UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        public FluentCustomerQuery _query;


        public ICustomerQuery Query()
        {
            _query = new FluentCustomerQuery();
            return _query as ICustomerQuery;
        }

        public ICustomer CreateCustomerEmail(CustomerView customerView)
        {
            Task<AddressBook> lookupAddressBookTask = Task.Run(() => unitOfWork.addressBookRepository.GetAddressBookByCustomerView(customerView));
            Task.WaitAll(lookupAddressBookTask);
            customerView.AddressId = lookupAddressBookTask.Result.AddressId;

            if (customerView.AddressId > 0)
            {
                EmailView emailView = new EmailView();
                emailView.AddressId = customerView.AddressId;
                emailView.EmailText = customerView.AccountEmail.EmailText;
                emailView.LoginEmail = customerView.AccountEmail.LoginEmail;
                emailView.Password = customerView.AccountEmail.Password;

                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.emailRepository.CreateEmail(emailView));
                processStatus = resultTask.Result;
            }
            else
            {
                processStatus = CreateProcessStatus.Failed;
            }

            return this as ICustomer;
        }
        public ICustomer CreateCustomer(CustomerView customerView)
        {
            try
            {
                Task<AddressBook> lookupAddressBookTask = Task.Run(() => unitOfWork.addressBookRepository.GetAddressBookByCustomerView(customerView));
                Task.WaitAll(lookupAddressBookTask);
                customerView.AddressId = lookupAddressBookTask.Result.AddressId;


                Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.customerRepository.CreateCustomer(customerView));
                Task.WaitAll(resultTask);

                processStatus = resultTask.Result;

                return this as ICustomer;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public ICustomer CreateCustomerLocationAddress(CustomerView customerView)
        {
            Task<AddressBook> lookupAddressBookTask = Task.Run(() => unitOfWork.addressBookRepository.GetAddressBookByCustomerView(customerView));
            Task.WaitAll(lookupAddressBookTask);

            customerView.AddressId = lookupAddressBookTask.Result.AddressId;

            Task<CreateProcessStatus> resultTask = Task.Run(() => unitOfWork.locationAddressRepository.CreateLocationUsingCustomer(customerView));
            Task.WaitAll(resultTask);

            return this as ICustomer;

        }
        public ICustomer CreateAddressBook(CustomerView customerView)
        {

            Task<CreateProcessStatus> statusTask = Task.Run(() => unitOfWork.addressBookRepository.CreateAddressBook(customerView));
            Task.WaitAll(statusTask);
            processStatus = statusTask.Result;
            return this as ICustomer;
        }
        public ICustomer Apply()
        {
            if ((processStatus == CreateProcessStatus.Inserted) || (processStatus == CreateProcessStatus.Updated) || (processStatus == CreateProcessStatus.Deleted))
            {
                unitOfWork.CommitChanges();
            }
            return this as ICustomer;

        }

    }
}
