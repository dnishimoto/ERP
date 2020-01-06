using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;
using System.Threading.Tasks;
using lssWebApi2.CustomerDomain;

namespace lssWebApi2.AddressBookDomain
{

public class FluentEmail :IFluentEmail
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentEmail() { }
        public IFluentEmailQuery Query()
        {
            return new FluentEmailQuery(unitOfWork) as IFluentEmailQuery;
        }
        public IFluentEmail Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentEmail;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentEmail CreateSupplierEmail(long ? addressId, EmailEntity emailEntity)
        {

             Task<EmailEntity> emailLookupTask = Task.Run(async () => await unitOfWork.emailRepository.FindAccountEmailbyAddressId(addressId,emailEntity.Email));
            Task.WaitAll(emailLookupTask);

            if (emailLookupTask.Result == null)

            {
                AddEmail(emailEntity);
                return this as IFluentEmail;
              
            }
            processStatus = CreateProcessStatus.AlreadyExists;
            return this as IFluentEmail;

        }
        public IFluentEmail CreateCustomerEmail(CustomerView customerView)
        {
            Task<AddressBook> lookupAddressBookTask = Task.Run(() => unitOfWork.addressBookRepository.GetAddressBookByCustomerView(customerView));
            Task.WaitAll(lookupAddressBookTask);
            customerView.AddressId = lookupAddressBookTask.Result.AddressId;

            if (customerView.AddressId > 0)
            {
                EmailEntity emailEntity = new EmailEntity();
                emailEntity.AddressId = customerView.AddressId;
                emailEntity.Email = customerView.AccountEmail;
                emailEntity.LoginEmail = customerView.AccountEmailLogin;
                emailEntity.Password = customerView.AccountEmailPassword;

                AddEmail(emailEntity);
                processStatus = CreateProcessStatus.Insert;
            }
            else
            {
                processStatus = CreateProcessStatus.Failed;
            }

            return this as IFluentEmail;
        }
        public IFluentEmail CreateEmailByAddressId(long ? addressId, EmailEntity emailEntity)
        {
            try
            {
                Task<EmailEntity> emailLookupTask = Task.Run(async () => await unitOfWork.emailRepository.FindAccountEmailbyAddressId(addressId,emailEntity.Email));
                Task.WaitAll(emailLookupTask);
                if (emailLookupTask.Result == null)

                {
                    emailEntity.LoginEmail = true;
                    AddEmail(emailEntity);
                    return this as IFluentEmail;
                }
                processStatus= CreateProcessStatus.AlreadyExists;
                return this as IFluentEmail;
            }
            catch (Exception ex) { throw new Exception("CreateEmailByAddressId", ex); }
        }
           public IFluentEmail AddEmailsByList(List<EmailEntity> newObjects)
        {
            unitOfWork.emailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentEmail;
        }
        public IFluentEmail UpdateEmailsByList(IList<EmailEntity> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.emailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentEmail;
        }
        public IFluentEmail AddEmail(EmailEntity newObject) {
            unitOfWork.emailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentEmail;
        }
        public IFluentEmail UpdateEmail(EmailEntity updateObject) {
            unitOfWork.emailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentEmail;

        }
        public IFluentEmail DeleteEmail(EmailEntity deleteObject) {
            unitOfWork.emailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentEmail;
        }
   	public IFluentEmail DeleteEmailsByList(List<EmailEntity> deleteObjects)
        {
            unitOfWork.emailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentEmail;
        }
    }
}
