using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.TimeAndAttendanceDomain
{
    public interface IFluentTimeAndAttendanceQuery
    {
        TimeAndAttendancePunchIn GetPunchInById(long timePunchinId);
        IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId);
        TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>>predicate);
    }
    public class FluentTimeAndAttendanceQuery : IFluentTimeAndAttendanceQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentTimeAndAttendanceQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate)
        {
           var query= _unitOfWork.TARepository.GetObjectsQueryable(predicate) as IQueryable<TimeAndAttendancePunchIn>;
            TimeAndAttendancePunchIn retItem=null;
            foreach (var item in query)
            {
                retItem = item;
                break;
            }
            return retItem;
          
        }

        public TimeAndAttendancePunchIn GetPunchInById(long timePunchinId)
        {

            Task<TimeAndAttendancePunchIn> taPunchinTask = Task.Run(async() => await _unitOfWork.TARepository.GetObjectAsync(timePunchinId));
            Task.WaitAll(taPunchinTask);
            return taPunchinTask.Result;
        }
        public IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId)
        {
            Task<IList<TimeAndAttendancePunchInView>> resultTask = Task.Run<IList<TimeAndAttendancePunchInView>>(async () => await _unitOfWork.TARepository.GetTAPunchinByEmployeeId(employeeId));
            return resultTask.Result;
        }
    }
    public interface ITimeAndAttendanceModule
    {
        IFluentTimeAndAttendanceQuery Query();
        ITimeAndAttendanceModule AddPunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendanceModule DeletePunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendanceModule UpdatePunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendanceModule Apply();
    }

    class TimeAndAttendanceModule : AbstractModule, ITimeAndAttendanceModule
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        CreateProcessStatus processStatus;

        public TimeAndAttendanceModule()
        {

        }

        public IFluentTimeAndAttendanceQuery Query()
        {
            return new FluentTimeAndAttendanceQuery(unitOfWork) as IFluentTimeAndAttendanceQuery;
        }

        public ITimeAndAttendanceModule Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as ITimeAndAttendanceModule;
        }
        public string FormatPunchDateTime(DateTime? punchinDate)
        {
            return unitOfWork.TARepository.GetPunchDateTime(punchinDate);
        }
        public ITimeAndAttendanceModule AddPunchIn(TimeAndAttendancePunchIn taPunchin)
        {

            try
            {
                Task<CreateProcessStatus> statusTask = Task.Run(async()=>await unitOfWork.TARepository.AddPunchin(taPunchin));
                Task.WaitAll(statusTask);

                //unitOfWork.CommitChanges();
                processStatus=statusTask.Result;
                return this as ITimeAndAttendanceModule;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        //public async Task<TimeAndAttendancePunchIn> GetPunchInById(long timePunchinId)
        //{
        //    try
        //    {

        //        TimeAndAttendancePunchIn taPunchin = await unitOfWork.TARepository.GetObjectAsync(timePunchinId);
        //        return taPunchin;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(GetMyMethodName(), ex);
        //    }
        //}
        //public IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId)
        //{
        //    Task<IList<TimeAndAttendancePunchInView>> resultTask = Task.Run<IList<TimeAndAttendancePunchInView>>(async () => await unitOfWork.TARepository.GetTAPunchinByEmployeeId(employeeId));
        //    return resultTask.Result;
        //}
        public ITimeAndAttendanceModule UpdatePunchIn(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                 unitOfWork.TARepository.UpdateObject(taPunchin);
                processStatus=CreateProcessStatus.Update;
                return this as ITimeAndAttendanceModule;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public ITimeAndAttendanceModule DeletePunchIn(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                CreateProcessStatus statusResult = unitOfWork.TARepository.DeletePunchin(taPunchin);
                processStatus = statusResult;
                return this as ITimeAndAttendanceModule;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
