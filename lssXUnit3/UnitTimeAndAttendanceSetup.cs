using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceSetupDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.TimeAndAttendanceSetupDomain
{

    public class UnitTimeAndAttendanceSetup
    {

        private readonly ITestOutputHelper output;

        public UnitTimeAndAttendanceSetup(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            TimeAndAttendanceSetupModule TimeAndAttendanceSetupMod = new TimeAndAttendanceSetupModule();

            TimeAndAttendanceSetupView view = new TimeAndAttendanceSetupView()
            {
                TimeZone = "Mountain Standard Time",
                DaylightSavings = true,
                Offset = -7
            };
            NextNumber nnNextNumber = await TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.Query().GetNextNumber();

            view.TimeAndAttendanceSetupNumber = nnNextNumber.NextNumberValue;

            TimeAndAttendanceSetup timeAndAttendanceSetup = await TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.Query().MapToEntity(view);

            TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.AddTimeAndAttendanceSetup(timeAndAttendanceSetup).Apply();

            TimeAndAttendanceSetup newTimeAndAttendanceSetup = await TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.Query().GetEntityByNumber(view.TimeAndAttendanceSetupNumber);

            Assert.NotNull(newTimeAndAttendanceSetup);

            newTimeAndAttendanceSetup.TimeZone = "Pacific Update";

            TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.UpdateTimeAndAttendanceSetup(newTimeAndAttendanceSetup).Apply();

            TimeAndAttendanceSetupView updateView = await TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.Query().GetViewById(newTimeAndAttendanceSetup.TimeAndAttendanceSetupId);

            Assert.Same(updateView.TimeZone, "Pacific Update");
            TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.DeleteTimeAndAttendanceSetup(newTimeAndAttendanceSetup).Apply();
            TimeAndAttendanceSetup lookupTimeAndAttendanceSetup = await TimeAndAttendanceSetupMod.TimeAndAttendanceSetup.Query().GetEntityById(view.TimeAndAttendanceSetupId);

            Assert.Null(lookupTimeAndAttendanceSetup);
        }



    }
}
