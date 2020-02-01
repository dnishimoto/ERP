using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.TimeAndAttendanceSetupDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{

public class FluentTimeAndAttendanceSetup :IFluentTimeAndAttendanceSetup
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentTimeAndAttendanceSetup(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentTimeAndAttendanceSetupQuery Query()
        {
            return new FluentTimeAndAttendanceSetupQuery(unitOfWork) as IFluentTimeAndAttendanceSetupQuery;
        }
        public IFluentTimeAndAttendanceSetup Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentTimeAndAttendanceSetup;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentTimeAndAttendanceSetup AddTimeAndAttendanceSetups(List<TimeAndAttendanceSetup> newObjects)
        {
            unitOfWork.timeAndAttendanceSetupRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceSetup;
        }
        public IFluentTimeAndAttendanceSetup UpdateTimeAndAttendanceSetups(IList<TimeAndAttendanceSetup> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.timeAndAttendanceSetupRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceSetup;
        }
        public IFluentTimeAndAttendanceSetup AddTimeAndAttendanceSetup(TimeAndAttendanceSetup newObject) {
            unitOfWork.timeAndAttendanceSetupRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceSetup;
        }
        public IFluentTimeAndAttendanceSetup UpdateTimeAndAttendanceSetup(TimeAndAttendanceSetup updateObject) {
            unitOfWork.timeAndAttendanceSetupRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceSetup;

        }
        public IFluentTimeAndAttendanceSetup DeleteTimeAndAttendanceSetup(TimeAndAttendanceSetup deleteObject) {
            unitOfWork.timeAndAttendanceSetupRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceSetup;
        }
   	public IFluentTimeAndAttendanceSetup DeleteTimeAndAttendanceSetups(List<TimeAndAttendanceSetup> deleteObjects)
        {
            unitOfWork.timeAndAttendanceSetupRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceSetup;
        }
    }
}
