using lssWebApi2.AbstractFactory;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.TimeAndAttendanceShiftDomain
{
    public class TimeAndAttendanceShiftModule : AbstractModule
    {
        public FluentTimeAndAttendanceShift TimeAndAttendanceShift = new FluentTimeAndAttendanceShift();
    }
}
