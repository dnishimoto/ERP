using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceScheduledToWorkDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{

    public class UnitTimeAndAttendanceScheduledToWork
    {

        private readonly ITestOutputHelper output;

        public UnitTimeAndAttendanceScheduledToWork(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            TimeAndAttendanceScheduledToWorkModule TimeAndAttendanceScheduledToWorkMod = new TimeAndAttendanceScheduledToWorkModule();
            TimeAndAttendanceSchedule schedule = await TimeAndAttendanceScheduledToWorkMod.Schedule.Query().GetEntityById(4);
            TimeAndAttendanceShift shift = await TimeAndAttendanceScheduledToWorkMod.Shift.Query().GetEntityById(schedule?.ShiftId);
            Employee employee = await TimeAndAttendanceScheduledToWorkMod.Employee.Query().GetEntityById(1);
            AddressBook addressBookEmployee = await TimeAndAttendanceScheduledToWorkMod.AddressBook.Query().GetEntityById(employee?.AddressId);

            Udc udcJobCode = await TimeAndAttendanceScheduledToWorkMod.Udc.Query().GetEntityById(25);
            Udc udcPayCode = await TimeAndAttendanceScheduledToWorkMod.Udc.Query().GetEntityById(16);
            Udc udcWorkedJobCode = await TimeAndAttendanceScheduledToWorkMod.Udc.Query().GetEntityById(25);

            string shiftStartTime = await TimeAndAttendanceScheduledToWorkMod.Shift.Query().BuildLongDate(schedule.StartDate, shift?.ShiftStartTime);
            string shiftEndTime = await TimeAndAttendanceScheduledToWorkMod.Shift.Query().BuildLongDate(schedule.EndDate, shift?.ShiftEndTime);

            TimeAndAttendanceScheduledToWorkView view = new TimeAndAttendanceScheduledToWorkView()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = addressBookEmployee?.Name,
                ScheduleId = schedule.ScheduleId,
                ScheduleName = schedule.ScheduleName,
                ScheduleStartDate = schedule.StartDate,
                ScheduleEndDate = schedule.EndDate,
                ShiftId = shift?.ShiftId,
                ShiftStartTime = shiftStartTime,
                ShiftEndTime = shiftEndTime,
                DurationHours = shift?.DurationHours ?? 0,
                DurationMinutes = shift?.DurationMinutes ?? 0,
                Monday = shift?.Monday,
                Tuesday = shift?.Tuesday,
                Wednesday = shift?.Wednesday,
                Thursday = shift?.Thursday,
                Friday = shift?.Friday,
                Saturday = shift?.Saturday,
                Sunday = shift?.Sunday,
                JobCodeXrefId=udcJobCode.XrefId,
                JobCode = udcJobCode?.KeyCode,
                PayCodeXrefId=udcPayCode.XrefId,
                PayCode = udcPayCode?.KeyCode,
                WorkedJobCodeXrefId=udcWorkedJobCode.XrefId,
                WorkedJobCode = udcWorkedJobCode?.KeyCode             
            };
            NextNumber nnNextNumber = await TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.Query().GetNextNumber();

            view.ScheduledToWorkNumber = nnNextNumber.NextNumberValue;

            TimeAndAttendanceScheduledToWork timeAndAttendanceScheduledToWork = await TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.Query().MapToEntity(view);

            TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.AddTimeAndAttendanceScheduledToWork(timeAndAttendanceScheduledToWork).Apply();

            TimeAndAttendanceScheduledToWork newTimeAndAttendanceScheduledToWork = await TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.Query().GetEntityByNumber(view.ScheduledToWorkNumber);

            Assert.NotNull(newTimeAndAttendanceScheduledToWork);

            newTimeAndAttendanceScheduledToWork.Note="Note Update";

            TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.UpdateTimeAndAttendanceScheduledToWork(newTimeAndAttendanceScheduledToWork).Apply();

            TimeAndAttendanceScheduledToWorkView updateView = await TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.Query().GetViewById(newTimeAndAttendanceScheduledToWork.ScheduledToWorkId);

            Assert.Same(updateView.Note, "Note Update");
            TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.DeleteTimeAndAttendanceScheduledToWork(newTimeAndAttendanceScheduledToWork).Apply();
            TimeAndAttendanceScheduledToWork lookupTimeAndAttendanceScheduledToWork = await TimeAndAttendanceScheduledToWorkMod.ScheduledToWork.Query().GetEntityById(view.ScheduledToWorkId);

            Assert.Null(lookupTimeAndAttendanceScheduledToWork);
        }



    }
}
