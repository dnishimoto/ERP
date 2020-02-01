using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.SupervisorDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.SupervisorDomain
{

public class FluentSupervisor :IFluentSupervisor
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentSupervisor(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentSupervisorQuery Query()
        {
            return new FluentSupervisorQuery(unitOfWork) as IFluentSupervisorQuery;
        }
        public IFluentSupervisor Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentSupervisor;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentSupervisor AddSupervisors(List<Supervisor> newObjects)
        {
            unitOfWork.supervisorRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupervisor;
        }
        public IFluentSupervisor UpdateSupervisors(IList<Supervisor> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.supervisorRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupervisor;
        }
        public IFluentSupervisor AddSupervisor(Supervisor newObject) {
            unitOfWork.supervisorRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupervisor;
        }
        public IFluentSupervisor UpdateSupervisor(Supervisor updateObject) {
            unitOfWork.supervisorRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupervisor;

        }
        public IFluentSupervisor DeleteSupervisor(Supervisor deleteObject) {
            unitOfWork.supervisorRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupervisor;
        }
   	public IFluentSupervisor DeleteSupervisors(List<Supervisor> deleteObjects)
        {
            unitOfWork.supervisorRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupervisor;
        }
    }
}
