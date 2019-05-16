using lssWebApi2.EntityFramework;

namespace ERP_Core2.Interfaces
{
    public interface IFluentTimeAndAttendance
    {
        IFluentTimeAndAttendanceQuery Query();
        IFluentTimeAndAttendance AddPunchIn(TimeAndAttendancePunchIn taPunchin);
        IFluentTimeAndAttendance DeletePunchIn(TimeAndAttendancePunchIn taPunchin);
        IFluentTimeAndAttendance UpdatePunchIn(TimeAndAttendancePunchIn taPunchin,int mealDeduction,int manualElapsedHours= 0, int manualElapsedMinutes = 0);
        IFluentTimeAndAttendance Apply();
    }
}
