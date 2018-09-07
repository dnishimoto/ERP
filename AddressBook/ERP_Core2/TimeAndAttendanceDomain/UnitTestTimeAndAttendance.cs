using ERP_Core2.AddressBookDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.TimeAndAttendanceDomain;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.ScheduleEventsDomain;
using MillenniumERP.Services;
using MillenniumERP.TimeAndAttendanceDomain;
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
        public void TestAddScheduledToWork()
        {
            int supervisorId = 1;

            AddressBookModule abMod=new AddressBookModule();

            List<EmployeeView> employeeViews = abMod.AddressBook.Query().GetEmployeesBySupervisorId(supervisorId);


            string scheduleName = "Schedule A";
            DateTime startDate = DateTime.Parse("9/10/2018");
            DateTime endDate = DateTime.Parse("9/14/2018");

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            TimeAndAttendanceSchedule schedule = taMod.TimeAndAttendanceSchedule.Query().GetScheduleByExpression(e=>e.ScheduleName==scheduleName && e.StartDate==startDate && e.EndDate==endDate);

            
        }
        [Fact]
        public void TestAddSchedule()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            long shiftId = 1;
            Task<TimeAndAttendanceShift> shiftTask = Task.Run(async()=> await unitOfWork.timeAndAttendanceRepository.GetShiftById(shiftId));
            Task.WaitAll(shiftTask);
            TimeAndAttendanceScheduleView view = new TimeAndAttendanceScheduleView();


            view.ScheduleName = "Schedule A";
            view.StartDate = DateTime.Parse("9/10/2018"); 
            view.EndDate = DateTime.Parse("9/14/2018"); 

            view.ShiftId = shiftTask.Result.ShiftId;
            view.ShiftName = shiftTask.Result.ShiftName;
            view.ShiftStartTime = shiftTask.Result.ShiftStartTime;
            view.ShiftEndTime = shiftTask.Result.ShiftEndTime;
            view.ScheduleGroup = "A";

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            taMod.TimeAndAttendanceSchedule.AddSchedule(view).Apply();

        }
        [Fact]
        public void TestAddTAPunchin()
        {


            //UnitOfWork unitOfWork = new UnitOfWork();
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


            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            string punchinDateTime = taMod.TimeAndAttendance.FormatPunchDateTime(taPunchin.PunchinDate);
            taPunchin.PunchinDateTime = punchinDateTime;

            taPunchin.EmployeeId = employeeId;
            taPunchin.JobCodeXrefId = jobCodeXrefId;
            taPunchin.SupervisorId = supervisorId;
            taPunchin.TypeOfTimeUdcXrefId = typeOfTimeUdcXrefId;
            taPunchin.ApprovingAddressId = approvingAddressId;
            taPunchin.PayCodeXrefId = payCodeXrefId;
            taPunchin.ScheduleId = scheduleId;


            taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();

            //TODO Get the new created punchin
            TimeAndAttendancePunchIn taPunchinLookUp = taMod.TimeAndAttendance.Query().GetPunchInByExpression(e => e.PunchinDateTime == punchinDateTime && e.EmployeeId == employeeId);

            //TimeAndAttendancePunchIn taPunchinLookUp=  taMod.Query().GetPunchInById(timePunchinId);

            taMod.TimeAndAttendance.DeletePunchIn(taPunchinLookUp).Apply();
 
            Assert.True(true);

        }
        [Fact]
        public void TestUpdateTAPunchin()
        {
            long timePunchinId = 3;
            //UnitOfWork unitOfWork = new UnitOfWork();

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            TimeAndAttendancePunchIn taPunchinLookUp = taMod.TimeAndAttendance.Query().GetPunchInById(timePunchinId);

            taPunchinLookUp.DurationInMinutes = 480;
            taPunchinLookUp.MealDurationInMinutes = 30;

             taMod.TimeAndAttendance.UpdatePunchIn(taPunchinLookUp).Apply();
       

            Assert.True(true);

        }
        [Fact]
        public void TestGetTAPunchin()
        {
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            int employeeId = 3;

            IList<TimeAndAttendancePunchInView> queryList = taMod.TimeAndAttendance.Query().GetTAPunchinByEmployeeId(employeeId);

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
