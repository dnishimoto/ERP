using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceScheduleDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{

    public class UnitTimeAndAttendanceSchedule
    {

        private readonly ITestOutputHelper output;

        public UnitTimeAndAttendanceSchedule(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            TimeAndAttendanceScheduleModule TimeAndAttendanceScheduleMod = new TimeAndAttendanceScheduleModule();
            TimeAndAttendanceShift shift = await TimeAndAttendanceScheduleMod.Shift.Query().GetEntityById(1);
            TimeAndAttendanceScheduleView view = new TimeAndAttendanceScheduleView()
            {
                ScheduleName = "Test Sched",
                StartDate = DateTime.Parse("12/30/2019"),
                EndDate = DateTime.Parse("1/3/2020"),
                ShiftId = shift.ShiftId,
                ShiftName = shift.ShiftName,
                ShiftStartTime = shift.ShiftStartTime,
                ShiftEndTime = shift.ShiftEndTime,
                ScheduleGroup = "A",
                DurationHours = shift.DurationHours,
                DurationMinutes = shift.DurationMinutes,
                Monday = shift.Monday,
                Tuesday = shift.Tuesday,
                Wednesday = shift.Wednesday,
                Thursday = shift.Thursday,
                Friday = shift.Friday,
                Saturday = shift.Saturday,
                Sunday = shift.Sunday

            };
            NextNumber nnNextNumber = await TimeAndAttendanceScheduleMod.Schedule.Query().GetNextNumber();

            view.TimeAndAttendanceScheduleNumber = nnNextNumber.NextNumberValue;

            TimeAndAttendanceSchedule timeAndAttendanceSchedule = await TimeAndAttendanceScheduleMod.Schedule.Query().MapToEntity(view);

            TimeAndAttendanceScheduleMod.Schedule.AddTimeAndAttendanceSchedule(timeAndAttendanceSchedule).Apply();

            TimeAndAttendanceSchedule newTimeAndAttendanceSchedule = await TimeAndAttendanceScheduleMod.Schedule.Query().GetEntityByNumber(view.TimeAndAttendanceScheduleNumber);

            Assert.NotNull(newTimeAndAttendanceSchedule);

            newTimeAndAttendanceSchedule.ScheduleName="Sched Test Update";

            TimeAndAttendanceScheduleMod.Schedule.UpdateTimeAndAttendanceSchedule(newTimeAndAttendanceSchedule).Apply();

            TimeAndAttendanceScheduleView updateView = await TimeAndAttendanceScheduleMod.Schedule.Query().GetViewById(newTimeAndAttendanceSchedule.ScheduleId);

            Assert.Same(updateView.ScheduleName, "Sched Test Update");
            TimeAndAttendanceScheduleMod.Schedule.DeleteTimeAndAttendanceSchedule(newTimeAndAttendanceSchedule).Apply();
            TimeAndAttendanceSchedule lookupTimeAndAttendanceSchedule = await TimeAndAttendanceScheduleMod.Schedule.Query().GetEntityById(view.ScheduleId);

            Assert.Null(lookupTimeAndAttendanceSchedule);
        }



    }
}
