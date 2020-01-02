

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.TimeAndAttendanceShiftDomain;

namespace lssWebApi2.TimeAndAttendanceShiftDomain
{ 

public interface IFluentTimeAndAttendanceShift
    {
        IFluentTimeAndAttendanceShiftQuery Query();
        IFluentTimeAndAttendanceShift Apply();
        IFluentTimeAndAttendanceShift AddTimeAndAttendanceShift(TimeAndAttendanceShift timeAndAttendanceShift);
        IFluentTimeAndAttendanceShift UpdateTimeAndAttendanceShift(TimeAndAttendanceShift timeAndAttendanceShift);
        IFluentTimeAndAttendanceShift DeleteTimeAndAttendanceShift(TimeAndAttendanceShift timeAndAttendanceShift);
     	IFluentTimeAndAttendanceShift UpdateTimeAndAttendanceShifts(List<TimeAndAttendanceShift> newObjects);
        IFluentTimeAndAttendanceShift AddTimeAndAttendanceShifts(List<TimeAndAttendanceShift> newObjects);
        IFluentTimeAndAttendanceShift DeleteTimeAndAttendanceShifts(List<TimeAndAttendanceShift> deleteObjects);
    }
}
