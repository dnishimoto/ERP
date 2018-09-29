using ERP_Core2.AbstractFactory;
using ERP_Core2.Interfaces;
using ERP_Core2.Services;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.entityframework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ERP_Core2.FluentAPI
{
    public class FluentTimeAndAttendanceScheduleQuery : AbstractModule, ITimeAndAttendanceScheduleQuery
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
}
