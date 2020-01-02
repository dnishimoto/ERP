using lssWebApi2.AbstractFactory;
using lssWebApi2.TimeAndAttendanceScheduleDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.TimeAndAttendanceShiftDomain;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public class TimeAndAttendanceScheduleModule : AbstractModule
    {
        public FluentTimeAndAttendanceSchedule Schedule = new FluentTimeAndAttendanceSchedule();
        public FluentTimeAndAttendanceShift Shift = new FluentTimeAndAttendanceShift();
    }
}
