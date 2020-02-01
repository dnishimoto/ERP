using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.JobCostTypeDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.JobCostTypeDomain
{

public class FluentJobCostType :IFluentJobCostType
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentJobCostType(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentJobCostTypeQuery Query()
        {
            return new FluentJobCostTypeQuery(unitOfWork) as IFluentJobCostTypeQuery;
        }
        public IFluentJobCostType Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentJobCostType;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentJobCostType AddJobCostTypes(List<JobCostType> newObjects)
        {
            unitOfWork.jobCostTypeRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobCostType;
        }
        public IFluentJobCostType UpdateJobCostTypes(List<JobCostType> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.jobCostTypeRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobCostType;
        }
        public IFluentJobCostType AddJobCostType(JobCostType newObject) {
            unitOfWork.jobCostTypeRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobCostType;
        }
        public IFluentJobCostType UpdateJobCostType(JobCostType updateObject) {
            unitOfWork.jobCostTypeRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobCostType;

        }
        public IFluentJobCostType DeleteJobCostType(JobCostType deleteObject) {
            unitOfWork.jobCostTypeRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobCostType;
        }
   	public IFluentJobCostType DeleteJobCostTypes(List<JobCostType> deleteObjects)
        {
            unitOfWork.jobCostTypeRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobCostType;
        }
    }
}
