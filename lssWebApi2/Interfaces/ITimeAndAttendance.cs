using lssWebApi2.entityframework;

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
