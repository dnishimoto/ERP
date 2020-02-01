using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.JobCostLedgerDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.JobCostLedgerDomain
{

public class FluentJobCostLedger :IFluentJobCostLedger
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentJobCostLedger(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentJobCostLedgerQuery Query()
        {
            return new FluentJobCostLedgerQuery(unitOfWork) as IFluentJobCostLedgerQuery;
        }
        public IFluentJobCostLedger Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentJobCostLedger;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentJobCostLedger AddJobCostLedgers(List<JobCostLedger> newObjects)
        {
            unitOfWork.jobCostLedgerRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobCostLedger;
        }
        public IFluentJobCostLedger UpdateJobCostLedgers(List<JobCostLedger> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.jobCostLedgerRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobCostLedger;
        }
        public IFluentJobCostLedger AddJobCostLedger(JobCostLedger newObject) {
            unitOfWork.jobCostLedgerRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobCostLedger;
        }
        public IFluentJobCostLedger UpdateJobCostLedger(JobCostLedger updateObject) {
            unitOfWork.jobCostLedgerRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobCostLedger;

        }
        public IFluentJobCostLedger DeleteJobCostLedger(JobCostLedger deleteObject) {
            unitOfWork.jobCostLedgerRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobCostLedger;
        }
   	public IFluentJobCostLedger DeleteJobCostLedgers(List<JobCostLedger> deleteObjects)
        {
            unitOfWork.jobCostLedgerRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobCostLedger;
        }
    }
}
