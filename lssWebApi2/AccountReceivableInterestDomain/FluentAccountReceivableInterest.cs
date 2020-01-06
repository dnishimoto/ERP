using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.AccountReceivableInterestDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.AccountReceivableInterestDomain
{

public class FluentAccountReceivableInterest :IFluentAccountReceivableInterest
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentAccountReceivableInterest() { }
        public IFluentAccountReceivableInterestQuery Query()
        {
            return new FluentAccountReceivableInterestQuery(unitOfWork) as IFluentAccountReceivableInterestQuery;
        }
        public IFluentAccountReceivableInterest Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAccountReceivableInterest;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentAccountReceivableInterest AddAccountReceivableInterests(List<AccountReceivableInterest> newObjects)
        {
            unitOfWork.accountReceivableInterestRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivableInterest;
        }
        public IFluentAccountReceivableInterest UpdateAccountReceivableInterests(IList<AccountReceivableInterest> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.accountReceivableInterestRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivableInterest;
        }
        public IFluentAccountReceivableInterest AddAccountReceivableInterest(AccountReceivableInterest newObject) {
            unitOfWork.accountReceivableInterestRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivableInterest;
        }
        public IFluentAccountReceivableInterest UpdateAccountReceivableInterest(AccountReceivableInterest updateObject) {
            unitOfWork.accountReceivableInterestRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivableInterest;

        }
        public IFluentAccountReceivableInterest DeleteAccountReceivableInterest(AccountReceivableInterest deleteObject) {
            unitOfWork.accountReceivableInterestRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivableInterest;
        }
   	public IFluentAccountReceivableInterest DeleteAccountReceivableInterests(List<AccountReceivableInterest> deleteObjects)
        {
            unitOfWork.accountReceivableInterestRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivableInterest;
        }
    }
}
