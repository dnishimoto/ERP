using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.TimeAndAttendanceShiftDomain
{

public class FluentTimeAndAttendanceShift :IFluentTimeAndAttendanceShift
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentTimeAndAttendanceShift() { }
        public IFluentTimeAndAttendanceShiftQuery Query()
        {
            return new FluentTimeAndAttendanceShiftQuery(unitOfWork) as IFluentTimeAndAttendanceShiftQuery;
        }
        public IFluentTimeAndAttendanceShift Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentTimeAndAttendanceShift;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentTimeAndAttendanceShift AddTimeAndAttendanceShifts(List<TimeAndAttendanceShift> newObjects)
        {
            unitOfWork.timeAndAttendanceShiftRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceShift;
        }
        public IFluentTimeAndAttendanceShift UpdateTimeAndAttendanceShifts(List<TimeAndAttendanceShift> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.timeAndAttendanceShiftRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceShift;
        }
        public IFluentTimeAndAttendanceShift AddTimeAndAttendanceShift(TimeAndAttendanceShift newObject) {
            unitOfWork.timeAndAttendanceShiftRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceShift;
        }
        public IFluentTimeAndAttendanceShift UpdateTimeAndAttendanceShift(TimeAndAttendanceShift updateObject) {
            unitOfWork.timeAndAttendanceShiftRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceShift;

        }
        public IFluentTimeAndAttendanceShift DeleteTimeAndAttendanceShift(TimeAndAttendanceShift deleteObject) {
            unitOfWork.timeAndAttendanceShiftRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceShift;
        }
   	public IFluentTimeAndAttendanceShift DeleteTimeAndAttendanceShifts(List<TimeAndAttendanceShift> deleteObjects)
        {
            unitOfWork.timeAndAttendanceShiftRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceShift;
        }
    }
}
