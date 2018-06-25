using ERP_Core2.EntityFramework;
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
        public void TestAddTAPunchin()
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
 
            Task<bool> result = Task.Run(async () => await unitOfWork.TARepository.AddPunchin(taPunchin));

            unitOfWork.CommitChanges();
            Assert.True(result.Result);

        }
        [Fact]
        public void TestUpdateTAPunchin()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
           
            int timePunchinId = 3;
            int WorkDurationInMinutes = 480;
            int MealDurationInMinutes = 30;

            Task<bool>result = Task.Run(async () => await unitOfWork.TARepository.UpdateByTimePunchinId(timePunchinId, WorkDurationInMinutes, MealDurationInMinutes));

          
            unitOfWork.CommitChanges();
            Assert.True(result.Result);

        }
        [Fact]
        public void TestGetTAPunchin()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            int employeeId = 3;
            Task<IList<TimeAndAttendancePunchInView>> resultTask = Task.Run<IList<TimeAndAttendancePunchInView>>(async () => await unitOfWork.TARepository.GetTAPunchinByEmployeeId(employeeId));

            IList<TimeAndAttendancePunchInView> list = new List<TimeAndAttendancePunchInView>();
            foreach (var item in resultTask.Result)
            {
                output.WriteLine($"{item.EmployeeId} Date: {item.PunchinDateTime} Duration: {item.DurationInMinutes}");
                list.Add(item);
            }
            var Employee = list.Where(e => e.EmployeeId == employeeId).FirstOrDefault();

            Assert.True(Employee != null);
        }
    }
}
