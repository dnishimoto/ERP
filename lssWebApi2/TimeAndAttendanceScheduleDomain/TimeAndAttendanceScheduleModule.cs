using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using lssWebApi2.Services;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public class TimeAndAttendanceScheduleModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentTimeAndAttendanceSchedule Schedule;
        public FluentTimeAndAttendanceShift Shift;
        public TimeAndAttendanceScheduleModule()
        {
            unitOfWork = new UnitOfWork();
            Schedule = new FluentTimeAndAttendanceSchedule(unitOfWork);
            Shift = new FluentTimeAndAttendanceShift(unitOfWork);
        }
    }
}
