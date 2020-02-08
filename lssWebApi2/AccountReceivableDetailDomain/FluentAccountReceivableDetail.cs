using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.AccountReceivableDetailDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.AccountReceivableDetailDomain
{

public class FluentAccountReceivableDetail :IFluentAccountReceivableDetail
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentAccountReceivableDetail(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentAccountReceivableDetailQuery Query()
        {
            return new FluentAccountReceivableDetailQuery(unitOfWork) as IFluentAccountReceivableDetailQuery;
        }
        public IFluentAccountReceivableDetail Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAccountReceivableDetail;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentAccountReceivableDetail AddAccountReceivableDetails(List<AccountReceivableDetail> newObjects)
        {
            unitOfWork.accountReceivableDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivableDetail;
        }
        public IFluentAccountReceivableDetail UpdateAccountReceivableDetails(List<AccountReceivableDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.accountReceivableDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivableDetail;
        }
        public IFluentAccountReceivableDetail AddAccountReceivableDetail(AccountReceivableDetail newObject) {
            unitOfWork.accountReceivableDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivableDetail;
        }
        public IFluentAccountReceivableDetail UpdateAccountReceivableDetail(AccountReceivableDetail updateObject) {
            unitOfWork.accountReceivableDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivableDetail;

        }
        public IFluentAccountReceivableDetail DeleteAccountReceivableDetail(AccountReceivableDetail deleteObject) {
            unitOfWork.accountReceivableDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivableDetail;
        }
   	public IFluentAccountReceivableDetail DeleteAccountReceivableDetails(List<AccountReceivableDetail> deleteObjects)
        {
            unitOfWork.accountReceivableDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivableDetail;
        }
    }
}
