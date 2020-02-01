using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lssWebApi2.Enumerations;
using lssWebApi2.EntityFramework;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public class FluentTimeAndAttendanceSchedule : AbstractModule, IFluentTimeAndAttendanceSchedule
    {
        UnitOfWork unitOfWork ;
        CreateProcessStatus processStatus;
        FluentTimeAndAttendanceScheduleQuery _query;
        public FluentTimeAndAttendanceSchedule(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentTimeAndAttendanceScheduleQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentTimeAndAttendanceScheduleQuery(unitOfWork);
            }
            return _query as IFluentTimeAndAttendanceScheduleQuery;
        }
        public IFluentTimeAndAttendanceSchedule Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentTimeAndAttendanceSchedule;
        }
        public IFluentTimeAndAttendanceSchedule AddTimeAndAttendanceSchedules(List<TimeAndAttendanceSchedule> newObjects)
        {
            unitOfWork.timeAndAttendanceScheduleRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceSchedule;
        }
        public IFluentTimeAndAttendanceSchedule UpdateTimeAndAttendanceSchedules(IList<TimeAndAttendanceSchedule> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.timeAndAttendanceScheduleRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceSchedule;
        }
        public IFluentTimeAndAttendanceSchedule AddTimeAndAttendanceSchedule(TimeAndAttendanceSchedule newObject)
        {
            unitOfWork.timeAndAttendanceScheduleRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentTimeAndAttendanceSchedule;
        }
        public IFluentTimeAndAttendanceSchedule UpdateTimeAndAttendanceSchedule(TimeAndAttendanceSchedule updateObject)
        {
            unitOfWork.timeAndAttendanceScheduleRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentTimeAndAttendanceSchedule;

        }
        public IFluentTimeAndAttendanceSchedule DeleteTimeAndAttendanceSchedule(TimeAndAttendanceSchedule deleteObject)
        {
            unitOfWork.timeAndAttendanceScheduleRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceSchedule;
        }
        public IFluentTimeAndAttendanceSchedule DeleteTimeAndAttendanceSchedules(List<TimeAndAttendanceSchedule> deleteObjects)
        {
            unitOfWork.timeAndAttendanceScheduleRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentTimeAndAttendanceSchedule;
        }
    }
}
