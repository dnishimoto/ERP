using ERP_Core2.EntityFramework;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentTimeAndAttendanceQuery
    {
        TimeAndAttendancePunchIn GetPunchInById(long timePunchinId);
        IList<TimeAndAttendancePunchInView> GetTAPunchinByEmployeeId(long employeeId);
        TimeAndAttendancePunchIn GetPunchInByExpression(Expression<Func<TimeAndAttendancePunchIn, bool>> predicate);
    }
}
