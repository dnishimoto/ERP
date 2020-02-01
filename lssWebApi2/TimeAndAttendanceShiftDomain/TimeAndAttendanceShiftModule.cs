using lssWebApi2.AbstractFactory;
using lssWebApi2.Services;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace lssWebApi2.TimeAndAttendanceShiftDomain
{
    public class TimeAndAttendanceShiftModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentTimeAndAttendanceShift TimeAndAttendanceShift;
        public TimeAndAttendanceShiftModule()
        {
            unitOfWork = new UnitOfWork();
            TimeAndAttendanceShift = new FluentTimeAndAttendanceShift(unitOfWork);
        }
    }
}
