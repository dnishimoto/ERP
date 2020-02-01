using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollEarnings :IFluentPayRollEarnings
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentPayRollEarnings(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPayRollEarningsQuery Query()
        {
            return new FluentPayRollEarningsQuery(unitOfWork) as IFluentPayRollEarningsQuery;
        }
        public IFluentPayRollEarnings Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollEarnings;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPayRollEarnings AddPayRollEarningss(List<PayRollEarnings> newObjects)
        {
            unitOfWork.payRollEarningsRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollEarnings;
        }
        public IFluentPayRollEarnings UpdatePayRollEarningss(IList<PayRollEarnings> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollEarningsRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollEarnings;
        }
        public IFluentPayRollEarnings AddPayRollEarnings(PayRollEarnings newObject) {
            unitOfWork.payRollEarningsRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollEarnings;
        }
        public IFluentPayRollEarnings UpdatePayRollEarnings(PayRollEarnings updateObject) {
            unitOfWork.payRollEarningsRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollEarnings;

        }
        public IFluentPayRollEarnings DeletePayRollEarnings(PayRollEarnings deleteObject) {
            unitOfWork.payRollEarningsRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollEarnings;
        }
   	public IFluentPayRollEarnings DeletePayRollEarningss(List<PayRollEarnings> deleteObjects)
        {
            unitOfWork.payRollEarningsRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollEarnings;
        }
    }
}
