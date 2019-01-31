using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ERP_Core2.Interfaces
{
    public interface IFluentTimeAndAttendanceQuery
    {
        TimeAndAttendancePunchIn GetPunchInById(long timePunchinId);
        IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId);
        TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate);
        List<TimeAndAttendanceView> GetTimeAndAttendanceViewsByDate(DateTime startDate, DateTime endDate);
        List<TimeAndAttendanceView> GetTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate);
        IEnumerable<TimeAndAttendancePunchIn> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber);

    }
}
