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
        public void TestUpdateTAPunchin()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
           
            int timePunchinId = 3;
            int WorkDurationInMinutes = 480;
            int MealDurationInMinutes = 30;

            Task<bool>result = Task.Run(async () => await unitOfWork.TARepository.UpdateByTimePunchinId(timePunchinId, WorkDurationInMinutes, MealDurationInMinutes));

          
            unitOfWork.CommitChanges();
          

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
