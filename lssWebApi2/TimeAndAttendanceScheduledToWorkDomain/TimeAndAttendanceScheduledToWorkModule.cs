using lssWebApi2.AbstractFactory;
using lssWebApi2.TimeAndAttendanceScheduledToWorkDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.TimeAndAttendanceScheduleDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using lssWebApi2.UDCDomain;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public class TimeAndAttendanceScheduledToWorkModule : AbstractModule
    {
        public FluentTimeAndAttendanceScheduledToWork ScheduledToWork = new FluentTimeAndAttendanceScheduledToWork();
        public FluentTimeAndAttendanceSchedule Schedule = new FluentTimeAndAttendanceSchedule();
        public FluentEmployee Employee = new FluentEmployee();
        public FluentTimeAndAttendanceShift Shift = new FluentTimeAndAttendanceShift();
        public FluentAddressBook AddressBook = new FluentAddressBook();
        public FluentUdc Udc = new FluentUdc();
    }
}
