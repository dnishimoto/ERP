using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{
    public class TimeAndAttendanceSetupModule : AbstractModule
    {
        public FluentTimeAndAttendanceSetup TimeAndAttendanceSetup = new FluentTimeAndAttendanceSetup();
    }
}
