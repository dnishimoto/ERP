using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;

namespace lssWebApi2.AddressBookDomain
{

public class FluentPhone :IFluentPhone
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentPhone(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPhoneQuery Query()
        {
            return new FluentPhoneQuery(unitOfWork) as IFluentPhoneQuery;
        }
        public IFluentPhone Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPhone;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPhone AddPhoness(List<PhoneEntity> newObjects)
        {
            unitOfWork.phoneRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPhone;
        }
        public IFluentPhone UpdatePhoness(IList<PhoneEntity> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.phoneRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPhone;
        }
        public IFluentPhone AddPhones(PhoneEntity newObject) {
            unitOfWork.phoneRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPhone;
        }
        public IFluentPhone UpdatePhones(PhoneEntity updateObject) {
            unitOfWork.phoneRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPhone;

        }
        public IFluentPhone DeletePhones(PhoneEntity deleteObject) {
            unitOfWork.phoneRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPhone;
        }
   	public IFluentPhone DeletePhoness(List<PhoneEntity> deleteObjects)
        {
            unitOfWork.phoneRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPhone;
        }
    }
}
