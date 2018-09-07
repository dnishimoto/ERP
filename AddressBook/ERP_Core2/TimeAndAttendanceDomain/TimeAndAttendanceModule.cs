using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.FluentAPI;
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
        TimeAndAttendanceSchedule GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate);
    }
    public class FluentTimeAndAttendanceScheduleQuery: AbstractModule, ITimeAndAttendanceScheduleQuery
    {
        UnitOfWork _unitOfWork = null;
        public FluentTimeAndAttendanceScheduleQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public TimeAndAttendanceSchedule GetScheduleByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate)
        {
            try
            {
                var query = _unitOfWork.timeAndAttendanceScheduleRepository.GetObjectsQueryable(predicate) as IQueryable<TimeAndAttendanceSchedule>;
                TimeAndAttendanceSchedule retItem = null;
                foreach (var item in query)
                {
                    retItem = item;
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
    public class TimeAndAttendanceModule 
    {
        public FluentTimeAndAttendance TimeAndAttendance = new FluentTimeAndAttendance();
        public FluentTimeAndAttendanceSchedule TimeAndAttendanceSchedule = new FluentTimeAndAttendanceSchedule();

    }
}
