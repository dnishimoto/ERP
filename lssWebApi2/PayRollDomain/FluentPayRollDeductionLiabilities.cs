using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;

namespace ERP_Core2.PayRollDomain
{

public class FluentPayRollDeductionLiabilities :IFluentPayRollDeductionLiabilities
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPayRollDeductionLiabilities() { }
        public IFluentPayRollDeductionLiabilitiesQuery Query()
        {
            return new FluentPayRollDeductionLiabilitiesQuery(unitOfWork) as IFluentPayRollDeductionLiabilitiesQuery;
        }
        public IFluentPayRollDeductionLiabilities Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollDeductionLiabilities;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPayRollDeductionLiabilities AddPayRollDeductionLiabilitiess(List<PayRollDeductionLiabilities> newObjects)
        {
            unitOfWork.payRollDeductionLiabilitiesRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollDeductionLiabilities;
        }
        public IFluentPayRollDeductionLiabilities UpdatePayRollDeductionLiabilitiess(List<PayRollDeductionLiabilities> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollDeductionLiabilitiesRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollDeductionLiabilities;
        }
        public IFluentPayRollDeductionLiabilities AddPayRollDeductionLiabilities(PayRollDeductionLiabilities newObject) {
            unitOfWork.payRollDeductionLiabilitiesRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollDeductionLiabilities;
        }
        public IFluentPayRollDeductionLiabilities UpdatePayRollDeductionLiabilities(PayRollDeductionLiabilities updateObject) {
            unitOfWork.payRollDeductionLiabilitiesRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollDeductionLiabilities;

        }
        public IFluentPayRollDeductionLiabilities DeletePayRollDeductionLiabilities(PayRollDeductionLiabilities deleteObject) {
            unitOfWork.payRollDeductionLiabilitiesRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollDeductionLiabilities;
        }
   	public IFluentPayRollDeductionLiabilities DeletePayRollDeductionLiabilitiess(List<PayRollDeductionLiabilities> deleteObjects)
        {
            unitOfWork.payRollDeductionLiabilitiesRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollDeductionLiabilities;
        }
    }
}
