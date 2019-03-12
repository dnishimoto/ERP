using ERP_Core2.AddressBookDomain;

using ERP_Core2.TimeAndAttendanceDomain;
using ERP_Core2.TimeAndAttendanceDomain.Repository;
using ERP_Core2.ScheduleEventsDomain;
using ERP_Core2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using lssWebApi2.EntityFramework;

using System.Threading;
using System.Collections;
using System.Linq.Expressions;
using lssWebApi2.Enumerations;
using X.PagedList;

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
        public async Task TestGetPunchOpenView2()
        {
            long employeeId = 1;
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchInView view = null;

            view = await taMod.TimeAndAttendance.Query().GetPunchOpenView(employeeId);


        }

        [Fact]
        public async Task TestGetPunchOpenView()
        {
            long employeeId = 1;
            DateTime asOfDate = DateTime.Now;
      
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchInView view = null;
            string account = "1200.215";
            TimeAndAttendancePunchIn taPunchin = null;

            taPunchin = await taMod.TimeAndAttendance.Query().BuildPunchin(employeeId, account);
            taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();

            view = await taMod.TimeAndAttendance.Query().GetPunchOpenView(employeeId);

            taPunchin = await taMod.TimeAndAttendance.Query().GetPunchInById(view.TimePunchinId??0);

            taMod.TimeAndAttendance.DeletePunchIn(taPunchin).Apply();

            Assert.True(view != null);
           
        }
        [Fact]
        public async Task TestAddbyElapsedTime()
        {
            long employeeId = 1;
            UnitOfWork unitOfWork = new UnitOfWork();

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchIn taPunchin = null;

            DateTime asOfDate = DateTime.Now;

            string account = "1200.215";
            int mealDeduction = 30;
            int manual_elapsedHours = 12;
            int manual_elapsedMinutes = 30;

            bool isOpen = await taMod.TimeAndAttendance.Query().IsPunchOpen(employeeId, asOfDate);

            if (isOpen == false)
            {
                taPunchin = await taMod.TimeAndAttendance.Query().BuildPunchin(employeeId, account);

                taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();
            }

            taPunchin = await taMod.TimeAndAttendance.Query().GetPunchOpen(employeeId);

            taMod.TimeAndAttendance.UpdatePunchIn(taPunchin, mealDeduction, manual_elapsedHours, manual_elapsedMinutes).Apply();

            taPunchin = await taMod.TimeAndAttendance.Query().GetPunchInById(taPunchin.TimePunchinId);

            Assert.NotNull(taPunchin.PunchinDate);
        }
        [Fact]
        public async Task TestPunchOut()
        {
            long employeeId = 1;
 

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            TimeAndAttendancePunchIn taPunchin = null;

            DateTime asOfDate = DateTime.Now;

            string account = "1200.215";
            int mealDeduction = 0;

            bool isOpen = await taMod.TimeAndAttendance.Query().IsPunchOpen(employeeId, asOfDate);

            if (isOpen == false)
            {
                taPunchin = await taMod.TimeAndAttendance.Query().BuildPunchin(employeeId, account);

                taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();
            }

            taPunchin = await taMod.TimeAndAttendance.Query().GetPunchOpen(employeeId);

            Thread.Sleep(60000);

            taMod.TimeAndAttendance.UpdatePunchIn(taPunchin, mealDeduction).Apply();

            taPunchin = await taMod.TimeAndAttendance.Query().GetPunchInById(taPunchin.TimePunchinId);

            Assert.NotNull(taPunchin.PunchinDate);

            // taMod.TimeAndAttendance.DeletePunchIn(taPunchin).Apply();

        }

        [Fact]
        public async Task TestAddTimeByDuration()
        {
            //No UTC lookup time
            long employeeId = 1;
  
            int hours = 9;
            int minutes = 0;
            DateTime workDay = DateTime.Now;
            string account = "1200.215";

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            bool isOpen = await taMod.TimeAndAttendance.Query().IsPunchOpen(employeeId, workDay);

            if (isOpen != false)
            {
                TimeAndAttendancePunchIn taPunchin = await taMod.TimeAndAttendance.Query().BuildByTimeDuration(employeeId, hours, minutes, workDay, account);
                taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();
            }

        }
        [Fact]
        public async Task TestScheduledToWorkPunchin()
        {
            long employeeId = 1;

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            DateTime asOfDate = DateTime.Now;

            string account = "1200.215";

            bool isOpen = await taMod.TimeAndAttendance.Query().IsPunchOpen(employeeId, asOfDate);

            if (isOpen == false)
            {
                TimeAndAttendancePunchIn taPunchin = await taMod.TimeAndAttendance.Query().BuildPunchin(employeeId, account);

                taMod.TimeAndAttendance.AddPunchIn(taPunchin).Apply();
            }

        }
        [Fact]
        public async Task TestMoveTo()
        {
            int pageSize = 1;
            int pageNumber = 1;
            int employeeId = 3;

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();


            Func<TimeAndAttendancePunchIn, bool> predicate = e => e.EmployeeId == employeeId && e.PunchinDate == DateTime.Parse("6/23/2018");
            Func<TimeAndAttendancePunchIn, object> order = e => e.PunchinDateTime;

            TimeAndAttendanceViewContainer container = await taMod.TimeAndAttendance.Query().GetTimeAndAttendanceViewsByPage(predicate, order, pageSize, pageNumber);

           
            foreach (var item in container.items)
            {
                output.WriteLine($"{item.EmployeeId} Date: {item.PunchinDateTime} Duration: {item.DurationInMinutes}");
            }
            Assert.True(container.TotalItemCount > 0);
          

        }

        [Fact]
        public void TestEnumeration()
        {
            List<TypeOfPayEnum> listTypeOfPay = new List<TypeOfPayEnum> {
                TypeOfPayEnum.Regular, TypeOfPayEnum.Overtime,TypeOfPayEnum.HolidayPay,
                TypeOfPayEnum.DoubleOverTime,TypeOfPayEnum.TwoHalfTime
            };

            string[] arrayTypeOfPay = Array.ConvertAll<TypeOfPayEnum, string>(listTypeOfPay.ToArray(), new Converter<TypeOfPayEnum, string>(EnumToString));

            Assert.Contains("Regular", arrayTypeOfPay);

        }
        public static String EnumToString(TypeOfPayEnum element)
        {
            return element.ToString();
        }

        [Fact]
        public async Task TestTimeAndAttendanceExecuteAsync()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                long id = 7;
                TimeAndAttendancePunchIn item2 = await unitOfWork.timeAndAttendanceRepository._dbContext.TimeAndAttendancePunchIn.FindAsync(id);
                List<TimeAndAttendancePunchIn> list = await unitOfWork.timeAndAttendanceRepository._dbContext.TimeAndAttendancePunchIn.Where(e => e.EmployeeId == 3).ToListAsync<TimeAndAttendancePunchIn>();

                //no getawaiter
                //  var item =(await unitOfWork.timeAndAttendanceRepository._dbContext.TimeAndAttendancePunchIn.Where(e => e.EmployeeId == 3).ExecuteAsync()).FirstOrDefault();
                var item = await Task.FromResult(unitOfWork.timeAndAttendanceRepository._dbContext.TimeAndAttendancePunchIn.Where(e => e.EmployeeId == 3).FirstOrDefault<TimeAndAttendancePunchIn>());
                var item3 = await unitOfWork.timeAndAttendanceRepository._dbContext.TimeAndAttendancePunchIn.Where(e => e.EmployeeId == 3).FirstOrDefaultAsync<TimeAndAttendancePunchIn>();
                //foreach (var item in query)
                //{
                output.WriteLine($"{item.EmployeeId} vDate: {item.PunchinDateTime} Duration: {item.DurationInMinutes}");
                //}
            }
            catch (Exception ex)
            {
                output.WriteLine($"{ex.InnerException}");
            }

        }

        public static IEnumerable<object[]> TAGetData(int numTests)
        {
            var allData = new List<object[]>
        {
            new object[] { 4, DateTime.Parse("10/12/2018"), DateTime.Now },
            //new object[] { -4, -6, -10 },
         
            
        };

            return allData.Take(numTests);
        }

        //[Fact]
        [Theory]
        [MemberData(nameof(TAGetData), parameters: 3)]


        public async Task TestTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate)
        {

            // long employeeId = 4;
            //DateTime startDate = DateTime.Parse("10/12/2018");
            //DateTime endDate = DateTime.Now;

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            List<TimeAndAttendanceView> list = await taMod.TimeAndAttendance.Query().GetTimeAndAttendanceViewsByIdAndDate(employeeId, startDate, endDate);

            if (list.Count > 0)
            {
                Assert.True(true);
            }

        }
        [Fact]
        public async Task TestTimeAndAttendanceViewsByDate()
        {
            DateTime startDate = DateTime.Parse("10/12/2018");
            DateTime endDate = DateTime.Now;

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            List<TimeAndAttendanceView> list = await taMod.TimeAndAttendance.Query().GetTimeAndAttendanceViewsByDate(startDate, endDate);

            if (list.Count > 0)
            {
                Assert.True(true);
            }

        }


        public class ScheduledToWorkData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "Schedule A", DateTime.Parse("2/18/2019"), DateTime.Parse("2/22/2019"), "Regular" };

            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory]
        [ClassData(typeof(ScheduledToWorkData))]
        //[InlineData("Schedule A",  "2/18/2019" ,  "2/22/2019" )]
        public void TestAddScheduledToWork(string scheduleName, DateTime startDate, DateTime endDate, string payCode)
        {
            int supervisorId = 2;

            AddressBookModule abMod = new AddressBookModule();

            List<EmployeeView> employeeViews = abMod.AddressBook.Query().GetEmployeesBySupervisorId(supervisorId);


            //string scheduleName = "Schedule A";
            //DateTime startDate = DateTime.Parse("2/11/2019");
            // DateTime endDate = DateTime.Parse("2/15/2019");

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            TimeAndAttendanceScheduleView scheduleView = taMod.TimeAndAttendanceSchedule.Query().GetScheduleByExpression(e => e.ScheduleName == scheduleName && e.StartDate == startDate && e.EndDate == endDate);

            IList<TimeAndAttendanceScheduledToWork> items = taMod.TimeAndAttendanceScheduleToWork.BuildScheduledToWork(scheduleView, employeeViews, payCode);

            taMod.TimeAndAttendanceScheduleToWork.AddScheduledToWork(items).Apply();

            Assert.True(items.Count > 0);

        }
        [Fact]
        public void TestAddSchedule()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            long shiftId = 1;
            Task<TimeAndAttendanceShift> shiftTask = Task.Run(async () => await unitOfWork.timeAndAttendanceRepository.GetShiftById(shiftId));
            Task.WaitAll(shiftTask);
            TimeAndAttendanceScheduleView view = new TimeAndAttendanceScheduleView();


            view.ScheduleName = "Schedule A";
            view.StartDate = DateTime.Parse("2/18/2019");
            view.EndDate = DateTime.Parse("2/22/2019");

            view.ShiftId = shiftTask.Result.ShiftId;
            view.ShiftName = shiftTask.Result.ShiftName;
            view.ShiftStartTime = shiftTask.Result.ShiftStartTime;
            view.ShiftEndTime = shiftTask.Result.ShiftEndTime;
            view.DurationHours = shiftTask.Result.DurationHours;
            view.DurationMinutes = shiftTask.Result.DurationMinutes;
            view.Monday = shiftTask.Result.Monday;
            view.Tuesday = shiftTask.Result.Tuesday;
            view.Wednesday = shiftTask.Result.Wednesday;
            view.Thursday = shiftTask.Result.Thursday;
            view.Friday = shiftTask.Result.Friday;
            view.Saturday = shiftTask.Result.Saturday;
            view.Sunday = shiftTask.Result.Sunday;

            view.ScheduleGroup = "A";

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            taMod.TimeAndAttendanceSchedule.AddSchedule(view).Apply();

            TimeAndAttendanceScheduleView scheduleView = taMod.TimeAndAttendanceSchedule.Query().GetScheduleByExpression(e => e.ScheduleName == view.ScheduleName && e.StartDate == view.StartDate && e.EndDate == view.EndDate);

            Assert.True(scheduleView != null);

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

            TimeAndAttendancePunchIn taPunchinLookUp = taMod.TimeAndAttendance.Query().GetPunchInByExpression(e => e.PunchinDateTime == punchinDateTime && e.EmployeeId == employeeId);

            //TimeAndAttendancePunchIn taPunchinLookUp=  taMod.Query().GetPunchInById(timePunchinId);

            taMod.TimeAndAttendance.DeletePunchIn(taPunchinLookUp).Apply();

            Assert.True(true);

        }
        [Fact]
        public async Task TestUpdateTAPunchin()
        {
            long timePunchinId = 3;
            //UnitOfWork unitOfWork = new UnitOfWork();

            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();

            TimeAndAttendancePunchIn taPunchinLookUp = await taMod.TimeAndAttendance.Query().GetPunchInById(timePunchinId);

            taPunchinLookUp.DurationInMinutes = 480;
            taPunchinLookUp.MealDurationInMinutes = 30;
            int mealDeduction = 0;

            taMod.TimeAndAttendance.UpdatePunchIn(taPunchinLookUp, mealDeduction).Apply();


            Assert.True(true);

        }
        [Fact]
        public async Task TestGetTAPunchin()
        {
            TimeAndAttendanceModule taMod = new TimeAndAttendanceModule();
            int employeeId = 3;

            IList<TimeAndAttendancePunchInView> queryList = await taMod.TimeAndAttendance.Query().GetTAPunchinByEmployeeId(employeeId);

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
