using ERP_Core2.EntityFramework;
using ERP_Core2.TimeAndAttendanceDomain;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace ERP_Core2.TimeAndAttendenceDomain
{

    public class UnitTestTimeAndAttendance
    {
        private readonly ITestOutputHelper output;
        public UnitTestTimeAndAttendance(ITestOutputHelper output)
        {
            this.output = output;

        }

        [Fact]
        public async Task TestAddTAPunchin()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            DateTime punchinDate = DateTime.Parse("6/24/2018 08:01:02");
            int employeeId = 3;
            int jobCodeXrefId = 31;
            int supervisorId = 1;
            int typeOfTimeUdcXrefId = 11;
            int approvingAddressId = 1;
            int payCodeXrefId = 16;
            int scheduleId = 1;

            TimeAndAttendancePunchIn taPunchin = new TimeAndAttendancePunchIn();
            taPunchin.PunchinDate = punchinDate;

            string punchinDateTime = unitOfWork.TARepository.GetPunchDateTime(taPunchin.PunchinDate);
            taPunchin.PunchinDateTime = punchinDateTime;

            taPunchin.EmployeeId = employeeId;
            taPunchin.JobCodeXrefId = jobCodeXrefId;
            taPunchin.SupervisorId = supervisorId;
            taPunchin.TypeOfTimeUdcXrefId = typeOfTimeUdcXrefId;
            taPunchin.ApprovingAddressId = approvingAddressId;
            taPunchin.PayCodeXrefId = payCodeXrefId;
            taPunchin.ScheduleId = scheduleId;

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            long timePunchinId = await taMod.AddPunchIn(taPunchin);

            TimeAndAttendancePunchIn taPunchinLookUp= await taMod.GetPunchInById(timePunchinId);

            bool result = taMod.DeletePunchIn(taPunchinLookUp);
 
            Assert.True(result);

        }
        [Fact]
        public async Task TestUpdateTAPunchin()
        {
            long timePunchinId = 3;
            //UnitOfWork unitOfWork = new UnitOfWork();

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            TimeAndAttendancePunchIn taPunchinLookUp = await taMod.GetPunchInById(timePunchinId);

            taPunchinLookUp.DurationInMinutes = 480;
            taPunchinLookUp.MealDurationInMinutes = 30;

            bool result = taMod.UpdatePunchIn(taPunchinLookUp);
       

            Assert.True(result);

        }
        [Fact]
        public void TestGetTAPunchin()
        {
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            int employeeId = 3;

            IList<TimeAndAttendancePunchInView> queryList = taMod.GetTAPunchinByEmployeeId(employeeId);

            IList<TimeAndAttendancePunchInView> list = new List<TimeAndAttendancePunchInView>();
            foreach (var item in queryList)
            {
                output.WriteLine($"{item.EmployeeId} Date: {item.PunchinDateTime} Duration: {item.DurationInMinutes}");
                list.Add(item);
            }
            var Employee = list.Where(e => e.EmployeeId == employeeId).FirstOrDefault();

            Assert.True(Employee != null);
        }
    }
}
