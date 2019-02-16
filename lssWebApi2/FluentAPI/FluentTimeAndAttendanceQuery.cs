using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendanceQuery : IFluentTimeAndAttendanceQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentTimeAndAttendanceQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TimeAndAttendancePunchIn> BuildPunchin(long employeeId)
        {

            return await _unitOfWork.timeAndAttendanceRepository.BuildPunchin(employeeId);
        }
        public IPagedList<TimeAndAttendancePunchIn> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber)
        {
            try
            {
                Task<IPagedList<TimeAndAttendancePunchIn>> listTask = Task.Run(async()=>await _unitOfWork.timeAndAttendanceRepository.GetTimeAndAttendanceViewsByPage(predicate, order, pageSize, pageNumber));

                Task.WaitAll(listTask);



                return listTask.Result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetTimeAndAttendanceViewsByPage", ex);
            }

        }
        public TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate)
        {
            var query = _unitOfWork.timeAndAttendanceRepository.GetObjectsQueryable(predicate) as IQueryable<TimeAndAttendancePunchIn>;
            TimeAndAttendancePunchIn retItem = null;
            foreach (var item in query)
            {
                retItem = item;
                break;
            }
            return retItem;

        }

        public TimeAndAttendancePunchIn GetPunchInById(long timePunchinId)
        {

            Task<TimeAndAttendancePunchIn> taPunchinTask = Task.Run(async () => await _unitOfWork.timeAndAttendanceRepository.GetObjectAsync(timePunchinId));
            Task.WaitAll(taPunchinTask);
            return taPunchinTask.Result;
        }

        public List<TimeAndAttendanceView> GetTimeAndAttendanceViewsByDate(DateTime startDate, DateTime endDate)
        {
            Task<List<TimeAndAttendanceView>> taPunchinTask = Task.Run(async () => await _unitOfWork.timeAndAttendanceRepository.GetTimeAndAttendanceViewsByDate(startDate, endDate));
            Task.WaitAll(taPunchinTask);

            return taPunchinTask.Result;
        }
        public List<TimeAndAttendanceView> GetTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate)
        {
            Task<List<TimeAndAttendanceView>> taPunchinTask = Task.Run(async()=> await _unitOfWork.timeAndAttendanceRepository.GetTimeAndAttendanceViewsByIdAndDate(employeeId, startDate, endDate));
            Task.WaitAll(taPunchinTask);
         
            return taPunchinTask.Result;
        }
        public IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId)
        {
            Task<IList<TimeAndAttendancePunchInView>> resultTask = Task.Run<IList<TimeAndAttendancePunchInView>>(async () => await _unitOfWork.timeAndAttendanceRepository.GetTAPunchinByEmployeeId(employeeId));
            return resultTask.Result;
        }
    }
}
