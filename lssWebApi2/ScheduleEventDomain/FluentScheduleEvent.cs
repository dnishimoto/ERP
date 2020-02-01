using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ScheduleEventDomain
{

public class FluentScheduleEvent :IFluentScheduleEvent
    {
 private UnitOfWork unitOfWork ;
        private CreateProcessStatus processStatus;

        public FluentScheduleEvent(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentScheduleEventQuery Query()
        {
            return new FluentScheduleEventQuery(unitOfWork) as IFluentScheduleEventQuery;
        }
        public IFluentScheduleEvent Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentScheduleEvent;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentScheduleEvent AddScheduleEvents(List<ScheduleEvent> newObjects)
        {
            unitOfWork.scheduleEventRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentScheduleEvent;
        }
        public IFluentScheduleEvent UpdateScheduleEvents(IList<ScheduleEvent> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.scheduleEventRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentScheduleEvent;
        }
        public IFluentScheduleEvent AddScheduleEvent(ScheduleEvent newObject) {
            unitOfWork.scheduleEventRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentScheduleEvent;
        }
        public IFluentScheduleEvent UpdateScheduleEvent(ScheduleEvent updateObject) {
            unitOfWork.scheduleEventRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentScheduleEvent;

        }
        public IFluentScheduleEvent DeleteScheduleEvent(ScheduleEvent deleteObject) {
            unitOfWork.scheduleEventRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentScheduleEvent;
        }
   	public IFluentScheduleEvent DeleteScheduleEvents(List<ScheduleEvent> deleteObjects)
        {
            unitOfWork.scheduleEventRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentScheduleEvent;
        }
    }
}
