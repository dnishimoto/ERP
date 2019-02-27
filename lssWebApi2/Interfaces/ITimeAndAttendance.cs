using lssWebApi2.EntityFramework;

namespace ERP_Core2.Interfaces
{
    public interface ITimeAndAttendance
    {
        IFluentTimeAndAttendanceQuery Query();
        ITimeAndAttendance AddPunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendance DeletePunchIn(TimeAndAttendancePunchIn taPunchin);
        ITimeAndAttendance UpdatePunchIn(TimeAndAttendancePunchIn taPunchin,int mealDeduction);
        ITimeAndAttendance Apply();
    }
}
