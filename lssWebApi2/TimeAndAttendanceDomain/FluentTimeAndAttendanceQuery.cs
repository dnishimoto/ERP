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

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public class FluentTimeAndAttendanceQuery : IFluentTimeAndAttendanceQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentTimeAndAttendanceQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long employeeId, int hours, int minutes, int mealDurationInMinutes, DateTime workDay, string account)
        {
            return await _unitOfWork.timeAndAttendanceRepository.BuildByTimeDuration(employeeId, hours, minutes, mealDurationInMinutes, workDay, account);
        }
        public async Task<TimeAndAttendanceTimeView> GetUTCAdjustedTime()
        {
            return await _unitOfWork.timeAndAttendanceRepository.GetUTCAdjustedTime();
        }
        
        public async Task<TimeAndAttendancePunchInView> GetPunchOpenView(long employeeId)
        {
            return await _unitOfWork.timeAndAttendanceRepository.GetPunchOpenView(employeeId);
        }
        public async Task<TimeAndAttendancePunchIn> GetPunchOpen(long employeeId)
        {
            return await _unitOfWork.timeAndAttendanceRepository.GetPunchOpen(employeeId);
        }
        
        public async Task<TimeAndAttendancePunchIn> IsPunchOpen(long employeeId, DateTime asOfDate)
        {
            return await _unitOfWork.timeAndAttendanceRepository.IsPunchOpen(employeeId,asOfDate);
        }
        public async Task<TimeAndAttendancePunchIn> BuildPunchin(long employeeId,string account,DateTime punchDate)
        {

            return await _unitOfWork.timeAndAttendanceRepository.BuildPunchin(employeeId,account,punchDate);
        }
        public TimeAndAttendancePunchInView MapToView(TimeAndAttendancePunchIn item)
        {
            TimeAndAttendancePunchInView view = _unitOfWork.timeAndAttendanceRepository.MapToView(item);
            return view;
        }
        public async Task<TimeAndAttendanceViewContainer> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber)
        {
            try
            {
                TimeAndAttendanceViewContainer container = await _unitOfWork.timeAndAttendanceRepository.GetTimeAndAttendanceViewsByPage(predicate, order, pageSize, pageNumber);

                return container;
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
        public async Task<TimeAndAttendancePunchInView> GetPunchInByIdView(long timePunchinId)
        {
            TimeAndAttendancePunchInView view = await _unitOfWork.timeAndAttendanceRepository.GetPunchInByIdView(timePunchinId);
            
            return view;
        }
        public async Task<TimeAndAttendancePunchIn> GetPunchInById(long timePunchinId)
        {

            TimeAndAttendancePunchIn taPunchinTask = await _unitOfWork.timeAndAttendanceRepository.GetObjectAsync(timePunchinId);

            return taPunchinTask;
        }

        public async Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByDate(DateTime startDate, DateTime endDate)
        {
            List<TimeAndAttendanceView> taPunchin =  await _unitOfWork.timeAndAttendanceRepository.GetTimeAndAttendanceViewsByDate(startDate, endDate);
            
            return taPunchin;
        }
        public async Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate)
        {
            List<TimeAndAttendanceView> taPunchin =  await _unitOfWork.timeAndAttendanceRepository.GetTimeAndAttendanceViewsByIdAndDate(employeeId, startDate, endDate);
           
         
            return taPunchin;
        }
        public async Task<IList<TimeAndAttendancePunchInView>> GetTAPunchinByEmployeeId(long employeeId)
        {
            IList<TimeAndAttendancePunchInView> result =  await _unitOfWork.timeAndAttendanceRepository.GetTAPunchinByEmployeeId(employeeId);
            return result;
        }
    }
}
