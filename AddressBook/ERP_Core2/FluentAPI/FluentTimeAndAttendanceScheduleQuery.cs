using ERP_Core2.AbstractFactory;
using ERP_Core2.EntityFramework;
using ERP_Core2.Interfaces;
using MillenniumERP.Services;
using MillenniumERP.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
