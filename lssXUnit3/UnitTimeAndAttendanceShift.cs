using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceShiftDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.TimeAndAttendanceShiftDomain
{

    public class UnitTimeAndAttendanceShift
    {

        private readonly ITestOutputHelper output;

        public UnitTimeAndAttendanceShift(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            TimeAndAttendanceShiftModule TimeAndAttendanceShiftMod = new TimeAndAttendanceShiftModule();

            TimeAndAttendanceShiftView view = new TimeAndAttendanceShiftView()
            {
                ShiftName = "Test Shift",
                ShiftStartTime = "800",
                ShiftEndTime = "1600",
                ShiftType = "8 Hour",
                DurationHours = 8,
                DurationMinutes = 0,
                Monday = true,
                Tuesday = true,
                Wednesday = true,
                Thursday = true,
                Friday = true,
                Saturday = false,
                Sunday = false
            };
            NextNumber nnNextNumber = await TimeAndAttendanceShiftMod.TimeAndAttendanceShift.Query().GetNextNumber();

            view.TimeAndAttendanceShiftNumber = nnNextNumber.NextNumberValue;

            TimeAndAttendanceShift timeAndAttendanceShift = await TimeAndAttendanceShiftMod.TimeAndAttendanceShift.Query().MapToEntity(view);

            TimeAndAttendanceShiftMod.TimeAndAttendanceShift.AddTimeAndAttendanceShift(timeAndAttendanceShift).Apply();

            TimeAndAttendanceShift newTimeAndAttendanceShift = await TimeAndAttendanceShiftMod.TimeAndAttendanceShift.Query().GetEntityByNumber(view.TimeAndAttendanceShiftNumber);

            Assert.NotNull(newTimeAndAttendanceShift);

            newTimeAndAttendanceShift.ShiftName= "Shift Name Update";
         

            TimeAndAttendanceShiftMod.TimeAndAttendanceShift.UpdateTimeAndAttendanceShift(newTimeAndAttendanceShift).Apply();

            TimeAndAttendanceShiftView updateView = await TimeAndAttendanceShiftMod.TimeAndAttendanceShift.Query().GetViewById(newTimeAndAttendanceShift.ShiftId);

            Assert.Same(updateView.ShiftName, "Shift Name Update");
            TimeAndAttendanceShiftMod.TimeAndAttendanceShift.DeleteTimeAndAttendanceShift(newTimeAndAttendanceShift).Apply();
            TimeAndAttendanceShift lookupTimeAndAttendanceShift = await TimeAndAttendanceShiftMod.TimeAndAttendanceShift.Query().GetEntityById(view.ShiftId);

            Assert.Null(lookupTimeAndAttendanceShift);
        }



    }
}
