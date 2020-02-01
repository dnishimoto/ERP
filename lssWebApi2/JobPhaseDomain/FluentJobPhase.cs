using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.JobPhaseDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.JobPhaseDomain
{

public class FluentJobPhase :IFluentJobPhase
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentJobPhase(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentJobPhaseQuery Query()
        {
            return new FluentJobPhaseQuery(unitOfWork) as IFluentJobPhaseQuery;
        }
        public IFluentJobPhase Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentJobPhase;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentJobPhase AddJobPhases(List<JobPhase> newObjects)
        {
            unitOfWork.jobPhaseRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobPhase;
        }
        public IFluentJobPhase UpdateJobPhases(List<JobPhase> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.jobPhaseRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobPhase;
        }
        public IFluentJobPhase AddJobPhase(JobPhase newObject) {
            unitOfWork.jobPhaseRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobPhase;
        }
        public IFluentJobPhase UpdateJobPhase(JobPhase updateObject) {
            unitOfWork.jobPhaseRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobPhase;

        }
        public IFluentJobPhase DeleteJobPhase(JobPhase deleteObject) {
            unitOfWork.jobPhaseRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobPhase;
        }
   	public IFluentJobPhase DeleteJobPhases(List<JobPhase> deleteObjects)
        {
            unitOfWork.jobPhaseRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobPhase;
        }
    }
}
