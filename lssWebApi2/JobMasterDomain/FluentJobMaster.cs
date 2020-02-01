using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.JobMasterDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.JobMasterDomain
{

public class FluentJobMaster :IFluentJobMaster
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentJobMaster(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentJobMasterQuery Query()
        {
            return new FluentJobMasterQuery(unitOfWork) as IFluentJobMasterQuery;
        }
        public IFluentJobMaster Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentJobMaster;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentJobMaster AddJobMasters(List<JobMaster> newObjects)
        {
            unitOfWork.jobMasterRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobMaster;
        }
        public IFluentJobMaster UpdateJobMasters(List<JobMaster> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.jobMasterRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobMaster;
        }
        public IFluentJobMaster AddJobMaster(JobMaster newObject) {
            unitOfWork.jobMasterRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentJobMaster;
        }
        public IFluentJobMaster UpdateJobMaster(JobMaster updateObject) {
            unitOfWork.jobMasterRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentJobMaster;

        }
        public IFluentJobMaster DeleteJobMaster(JobMaster deleteObject) {
            unitOfWork.jobMasterRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobMaster;
        }
   	public IFluentJobMaster DeleteJobMasters(List<JobMaster> deleteObjects)
        {
            unitOfWork.jobMasterRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentJobMaster;
        }
    }
}
