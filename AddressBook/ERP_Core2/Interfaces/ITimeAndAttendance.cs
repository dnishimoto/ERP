using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface ITimeAndAttendance
    {
        IFluentTimeAndAttendanceQuery Query();
        ITimeAndAttendance AddPunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendance DeletePunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendance UpdatePunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendance Apply();
    }
}
