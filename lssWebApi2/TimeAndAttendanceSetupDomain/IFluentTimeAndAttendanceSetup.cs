

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{
    public interface IFluentTimeAndAttendanceSetup
    {
        IFluentTimeAndAttendanceSetupQuery Query();
        IFluentTimeAndAttendanceSetup Apply();
        IFluentTimeAndAttendanceSetup AddTimeAndAttendanceSetup(TimeAndAttendanceSetup timeAndAttendanceSetup);
        IFluentTimeAndAttendanceSetup UpdateTimeAndAttendanceSetup(TimeAndAttendanceSetup timeAndAttendanceSetup);
        IFluentTimeAndAttendanceSetup DeleteTimeAndAttendanceSetup(TimeAndAttendanceSetup timeAndAttendanceSetup);
        IFluentTimeAndAttendanceSetup UpdateTimeAndAttendanceSetups(IList<TimeAndAttendanceSetup> newObjects);
        IFluentTimeAndAttendanceSetup AddTimeAndAttendanceSetups(List<TimeAndAttendanceSetup> newObjects);
        IFluentTimeAndAttendanceSetup DeleteTimeAndAttendanceSetups(List<TimeAndAttendanceSetup> deleteObjects);
    }
}
