using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
using ERP_Core2.TimeAndAttendanceDomain.Repository;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.Services;
using MillenniumERP.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.TimeAndAttendanceDomain
{
    public interface ITimeAndAttendanceScheduleQuery
    {
        TimeAndAttendanceScheduleView GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate);
    }
    public class FluentTimeAndAttendanceScheduleQuery: AbstractModule, ITimeAndAttendanceScheduleQuery
    {
        UnitOfWork _unitOfWork = null;
        public FluentTimeAndAttendanceScheduleQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public TimeAndAttendanceScheduleView GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate)
        {
            try
            {
                var query = _unitOfWork.timeAndAttendanceScheduleRepository.GetObjectsQueryable(predicate) as IQueryable<TimeAndAttendanceSchedule>;
                TimeAndAttendanceScheduleView retItem = null;
                foreach (var item in query)
                {
                    retItem = _unitOfWork.timeAndAttendanceScheduleRepository.BuildTimeAndAttendanceScheduleView(item);
                    break;
                }
                return retItem;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

    }
    public interface ITimeAndAttendanceSchedule
    {
        ITimeAndAttendanceSchedule AddSchedule(TimeAndAttendanceScheduleView view);
        ITimeAndAttendanceSchedule Apply();
        ITimeAndAttendanceScheduleQuery Query();

    }
    public class FluentTimeAndAttendanceSchedule : AbstractModule, ITimeAndAttendanceSchedule
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        FluentTimeAndAttendanceScheduleQuery _query;
        public FluentTimeAndAttendanceSchedule()
        {

        }
        public ITimeAndAttendanceScheduleQuery Query()
        {
            if (_query == null)
            {
                _query = new FluentTimeAndAttendanceScheduleQuery(unitOfWork);
            }
            return _query as ITimeAndAttendanceScheduleQuery;
        }
        public ITimeAndAttendanceSchedule Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as ITimeAndAttendanceSchedule;
        }
        public ITimeAndAttendanceSchedule AddSchedule(TimeAndAttendanceScheduleView view)
        {
            Task<CreateProcessStatus> statusTask = Task.Run(async () => await unitOfWork.timeAndAttendanceScheduleRepository.AddSchedule(view));
            Task.WaitAll(statusTask);
            processStatus = statusTask.Result;
            return this as ITimeAndAttendanceSchedule;
        }
    }

    public interface ITimeAndAttendanceScheduledToWork
    {
        ITimeAndAttendanceScheduledToWork Apply();
        ITimeAndAttendanceScheduledToWork AddScheduledToWork(IList<TimeAndAttendanceScheduledToWork> items);
        IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews);
    }
    public class FluentTimeAndAttendanceScheduledToWork : AbstractModule, ITimeAndAttendanceScheduledToWork
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;
        public FluentTimeAndAttendanceScheduledToWork() { }
        public ITimeAndAttendanceScheduledToWork Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as ITimeAndAttendanceScheduledToWork;
        }
        public ITimeAndAttendanceScheduledToWork AddScheduledToWork(IList<TimeAndAttendanceScheduledToWork> items)
        {
            Task<CreateProcessStatus> resultTask=Task.Run(async()=>await unitOfWork.timeAndAttendanceScheduledToWorkRepository.AddScheduledToWorkItems(items));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as ITimeAndAttendanceScheduledToWork;
        }
        public IList<TimeAndAttendanceScheduledToWork> BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, IList<EmployeeView> employeeViews)
        {
            IList<TimeAndAttendanceScheduledToWork> retList = new List<TimeAndAttendanceScheduledToWork>();

            foreach (var employeeItem in employeeViews)
            {
                TimeAndAttendanceScheduledToWork scheduledToWork=unitOfWork.timeAndAttendanceScheduledToWorkRepository.BuildScheduledToWork(scheduleView, employeeItem);
                retList.Add(scheduledToWork);
            }

            return retList;
        }
    }
    public class TimeAndAttendanceModule 
    {
        public FluentTimeAndAttendance TimeAndAttendance = new FluentTimeAndAttendance();
        public FluentTimeAndAttendanceSchedule TimeAndAttendanceSchedule = new FluentTimeAndAttendanceSchedule();
        public FluentTimeAndAttendanceScheduledToWork TimeAndAttendanceScheduleToWork =new FluentTimeAndAttendanceScheduledToWork();
    }
}
