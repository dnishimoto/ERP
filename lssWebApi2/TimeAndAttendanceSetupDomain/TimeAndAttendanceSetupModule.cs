using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{
    public class TimeAndAttendanceSetupModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentTimeAndAttendanceSetup TimeAndAttendanceSetup;
        public TimeAndAttendanceSetupModule()
        {
            unitOfWork = new UnitOfWork();
            TimeAndAttendanceSetup = new FluentTimeAndAttendanceSetup(unitOfWork);
        }
    }
}
