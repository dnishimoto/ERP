using lssWebApi2.AbstractFactory;
using lssWebApi2.TimeAndAttendanceScheduledToWorkDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.TimeAndAttendanceScheduleDomain;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.Services;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public class TimeAndAttendanceScheduledToWorkModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentTimeAndAttendanceScheduledToWork ScheduledToWork;
        public FluentTimeAndAttendanceSchedule Schedule;
        public FluentEmployee Employee;
        public FluentTimeAndAttendanceShift Shift;
        public FluentAddressBook AddressBook;
        public FluentUdc Udc;

        public TimeAndAttendanceScheduledToWorkModule()
        {
            unitOfWork = new UnitOfWork();
            ScheduledToWork = new FluentTimeAndAttendanceScheduledToWork(unitOfWork);
            Schedule = new FluentTimeAndAttendanceSchedule(unitOfWork);
            Employee = new FluentEmployee(unitOfWork);
            Shift = new FluentTimeAndAttendanceShift(unitOfWork);
            AddressBook = new FluentAddressBook(unitOfWork);
            Udc = new FluentUdc(unitOfWork);

        }

    }
}
