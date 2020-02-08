using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;

namespace lssWebApi2.AccountPayableDetailDomain
{

public class FluentAccountPayableDetail :IFluentAccountPayableDetail
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentAccountPayableDetail(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentAccountPayableDetailQuery Query()
        {
            return new FluentAccountPayableDetailQuery(unitOfWork) as IFluentAccountPayableDetailQuery;
        }
        public IFluentAccountPayableDetail Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAccountPayableDetail;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentAccountPayableDetail AddAccountPayableDetails(List<AccountPayableDetail> newObjects)
        {
            unitOfWork.accountPayableDetailRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountPayableDetail;
        }
        public IFluentAccountPayableDetail UpdateAccountPayableDetails(List<AccountPayableDetail> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.accountPayableDetailRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountPayableDetail;
        }
        public IFluentAccountPayableDetail AddAccountPayableDetail(AccountPayableDetail newObject) {
            unitOfWork.accountPayableDetailRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountPayableDetail;
        }
        public IFluentAccountPayableDetail UpdateAccountPayableDetail(AccountPayableDetail updateObject) {
            unitOfWork.accountPayableDetailRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountPayableDetail;

        }
        public IFluentAccountPayableDetail DeleteAccountPayableDetail(AccountPayableDetail deleteObject) {
            unitOfWork.accountPayableDetailRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountPayableDetail;
        }
   	public IFluentAccountPayableDetail DeleteAccountPayableDetails(List<AccountPayableDetail> deleteObjects)
        {
            unitOfWork.accountPayableDetailRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountPayableDetail;
        }
    }
}
