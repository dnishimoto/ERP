using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendanceQuery : IFluentTimeAndAttendanceQuery
    {
        private UnitOfWork _unitOfWork;
        public FluentTimeAndAttendanceQuery(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return _unitOfWork.timeAndAttendanceRepository.GetTimeAndAttendanceViewsByDate(startDate,endDate);
        }
        public IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId)
        {
            Task<IList<TimeAndAttendancePunchInView>> resultTask = Task.Run<IList<TimeAndAttendancePunchInView>>(async () => await _unitOfWork.timeAndAttendanceRepository.GetTAPunchinByEmployeeId(employeeId));
            return resultTask.Result;
        }
    }
}
