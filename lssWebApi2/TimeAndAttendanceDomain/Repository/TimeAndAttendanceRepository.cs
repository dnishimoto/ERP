using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using lssWebApi2.EntityFramework;
using X.PagedList;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceDomain.Repository;
using lssWebApi2.AbstractFactory;

namespace ERP_Core2.TimeAndAttendanceDomain
{

    public class TimeAndAttendancePunchInView
    {
        public TimeAndAttendancePunchInView() { }
        public TimeAndAttendancePunchInView(TimeAndAttendancePunchIn taPunchin)
        {
            this.TimePunchinId = taPunchin.TimePunchinId;
            this.PunchinDate = taPunchin.PunchinDate;
            this.PunchoutDate = taPunchin.PunchoutDate;
            this.PunchinDateTime = taPunchin.PunchinDateTime;
            this.PunchoutDateTime = taPunchin.PunchoutDateTime;
            this.JobCodeXrefId = taPunchin.JobCodeXrefId;
            this.JobCode = taPunchin.JobCode;
            this.Approved = taPunchin.Approved;
            this.EmployeeId = taPunchin.EmployeeId;
            this.EmployeeName = taPunchin.Employee.Address.Name;
            this.SupervisorId = taPunchin.SupervisorId;
            this.SupervisorName = taPunchin.SupervisorNavigation?.Address?.Name;
            this.ProcessedDate = taPunchin.ProcessedDate;
            this.Note = taPunchin.Note;
            this.ShiftId = taPunchin.ShiftId;
            this.ScheduledToWork = taPunchin.ScheduledToWork;
            this.TypeOfTimeUdcXrefId = taPunchin.TypeOfTimeUdcXrefId;
            this.TypeOfTime = taPunchin.TypeOfTimeUdcXref.Value;
            this.ApprovingAddressId = taPunchin.ApprovingAddressId;
            this.PayCodeXrefId = taPunchin.PayCodeXrefId;
            this.PayCode = taPunchin.PayCode;
            this.ScheduleId = taPunchin.ScheduleId;
            this.DurationInMinutes = taPunchin.DurationInMinutes??0;
            this.MealDurationInMinutes = taPunchin.MealDurationInMinutes ?? 0;
            this.AreaCode = taPunchin.AreaCode ?? "";
            this.DepartmentCode = taPunchin.DepartmentCode ?? "";
        }

        public long? TimePunchinId { get; set; }
        public DateTime? PunchinDate { get; set; }
        public DateTime? PunchoutDate { get; set; }
        public string PunchinDateTime { get; set; }
        public string PunchoutDateTime { get; set; }
        public long? JobCodeXrefId { get; set; }
        public string JobCode { get; set; }
        public bool? Approved { get; set; }
        public long? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public long? SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string Note { get; set; }
        public long? ShiftId { get; set; }
        public bool? ScheduledToWork { get; set; }
        public long? TypeOfTimeUdcXrefId { get; set; }
        public string TypeOfTime { get; set; }
        public long? ApprovingAddressId { get; set; }
        public long? PayCodeXrefId { get; set; }
        public string PayCode { get; set; }
        public long? ScheduleId { get; set; }
        public int? DurationInMinutes { get; set; }
        public int? MealDurationInMinutes { get; set; }
        public string AreaCode { get; set; }
        public string DepartmentCode { get; set; }

    }

    public class TimeAndAttendanceView
    {
        public long TimePunchinId { get; set; }
        public DateTime? PunchinDate { get; set; }
        public DateTime? PunchoutDate { get; set; }
        public string PunchinDateTime { get; set; }
        public string PunchoutDateTime { get; set; }
        public string PayCode { get; set; }
        public string TypeOfTime { get; set; }
        public string JobCode { get; set; }
        public long ApproverAddressId { get; set; }
        public string ApproverName { get; set; }
        public string EmployeeName { get; set; }
        public long EmployeeId { get; set; }
        public long? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string ShiftType { get; set; }
        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime? ScheduleStartDate { get; set; }
        public DateTime? ScheduleEndDate { get; set; }
        public string ScheduleGroup { get; set; }
        public bool ScheduledToWork { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }

    }
    public class TimeAndAttendanceTimeView
    {
        public DateTime PunchDate { get; set; }
        public String PunchDateTime { get; set; }
    }
    public class TimeAndAttendanceParam
    {
        public long EmployeeId { get; set; }
        public string Account { get; set; }
        public int MealDeduction { get; set; }
        public DateTime AsOfDate { get; set; }
        public int Manual_ElapsedHours { get; set; }
        public int Manual_ElapsedMinutes { get; set; }
    }
    public class TimeAndAttendanceViewContainer: AbstractViewContainer
    {
        
        public TimeAndAttendanceViewContainer() {
            items = new List<TimeAndAttendancePunchInView>();
        }
        public List<TimeAndAttendancePunchInView> items { get; set; }
    }
    public static class Utilities
    {
        public static string Right(this string sValue, int iMaxLength)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(sValue))
            {
                //Set valid empty string as string could be null
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                //Make the string no longer than the max length
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            //Return the string
            return sValue;
        }

    }

    public class TimeAndAttendanceRepository : Repository<TimeAndAttendancePunchIn>,ITimeAndAttendanceRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public TimeAndAttendancePunchInView MapToView(TimeAndAttendancePunchIn item)
        {
            TimeAndAttendancePunchInView view = applicationViewFactory.MapTAPunchinView(item);
            return view;
        }
        public async Task<TimeAndAttendanceViewContainer> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber)
        {
            IEnumerable<TimeAndAttendancePunchIn> query = _dbContext.TimeAndAttendancePunchIn
                          .Where(predicate).OrderByDescending(order).Select(e => e);

            IPagedList<TimeAndAttendancePunchIn> list = await query.ToPagedListAsync(pageNumber, pageSize);

            TimeAndAttendanceViewContainer container = new TimeAndAttendanceViewContainer();
            container.PageNumber = pageNumber;
            container.PageSize = pageSize;
            container.TotalItemCount = list.TotalItemCount;

            foreach (var item in list)
            {
                TimeAndAttendancePunchInView view = MapToView(item);
                container.items.Add(view);
            }
            //await Task.Yield();
            return container;

        }
        public async Task<TimeAndAttendancePunchIn> BuildByTimeDuration(long employeeId, int hours, int minutes, int mealDurationInMinutes, DateTime workDay, string account)
        {

            string workDayDateTime = BuildLongDate(workDay);

            TimeAndAttendancePunchIn retTA = await GetTimeAndAttendancePunchIn(employeeId, account, workDay, workDayDateTime, hoursDuration: hours, minutesDuration: minutes,mealDurationInMinutes: mealDurationInMinutes);

            Task<Udc> statusTask = GetUdc("TA_STATUS", TypeOfTAStatus.Closed.ToString().ToUpper());
            Task.WaitAll(statusTask);
            retTA.TaskStatusXrefId = statusTask.Result.XrefId;
            retTA.TaskStatus = statusTask.Result.KeyCode;
            return retTA;
        }
        private string ReverseDate(Object pattern)
        {
            string sYear, sMonth, sDay;
            DateTime dDate;

            try
            {
                if (String.IsNullOrEmpty(pattern.ToString()) == true)
                {
                    return ("");
                }
                else if (pattern.ToString() == "")
                {
                    return ("");
                }

                sYear = pattern.ToString().Substring(0, 4);
                sMonth = pattern.ToString().Substring(4, 2);
                sDay = pattern.ToString().Substring(6, 2);

                dDate = DateTime.Parse(sMonth + "/" + sDay + "/" + sYear);
                return (dDate.ToString("MM/dd/yyyy"));
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        private string FormatTime(string hours, string minutes, string seconds)
        {
            int temp;
            string retVal = "";

            if (hours != "") {
                if (Int32.Parse(hours) >= 13)
                {
                    temp = Int32.Parse(hours) - 12;
                    retVal = temp.ToString() + ":" + minutes + ":" + seconds + " PM";
                }
                else if (Int32.Parse(hours) == 12)
                {
                    retVal = Int32.Parse(hours) + ":" + minutes + ":" + seconds + " PM";
                }
                else
                {
                    if (Int32.Parse(hours) < 1)
                    {
                        temp = Int32.Parse(hours) + 12;
                        retVal = temp.ToString() + ":" + minutes + ":" + seconds + " AM";
                    }
                    else
                    {
                        retVal = Int32.Parse(hours) + ":" + minutes + ":" + seconds + " AM";
                    }

                }
            }
            return (retVal);
        }
        private string ReverseTranslateShort24hr(string pattern)
        {
            string retVal;
            string hours;
            string minutes;
            string seconds;

            hours = pattern.ToString().Substring(0, 2);
            minutes = pattern.ToString().Substring(2, 2);
            seconds = "00";

            retVal = FormatTime(hours, minutes, seconds);

            return (retVal);
        }
        private string ReverseTranslate24hr(object pattern)
        {
            string retVal = "";
            string year;
            string month;
            string day;
            string hours;
            string minutes;
            string seconds;

            if (String.IsNullOrEmpty(pattern.ToString()))
            {
                return (retVal);
            }

            year = pattern.ToString().Substring(0, 4);
            month = pattern.ToString().Substring(4, 2);
            day = pattern.ToString().Substring(6, 2);
            hours = pattern.ToString().Substring(8, 2);
            minutes = pattern.ToString().Substring(10, 2);
            seconds = pattern.ToString().Substring(12, 2);

            retVal = FormatTime(hours, minutes, seconds);

            return (retVal);
        }
        private async Task<TimeAndAttendanceTimeView> GetTimeByMinuteDuration(string punchinString, int minutesDuration)
        {
            DateTime fromTime;
            DateTime toTime;
            string timeIn = "";
            string timeOut = "";

            try
            {
                timeIn = ReverseDate(punchinString) + " " + ReverseTranslate24hr(punchinString);
                fromTime = DateTime.Parse(timeIn);

                toTime = fromTime.AddMinutes(minutesDuration);

                timeOut = GetPunchDateTime(toTime);

                TimeAndAttendanceTimeView myTime = new TimeAndAttendanceTimeView();

                myTime.PunchDate = toTime;
                myTime.PunchDateTime = timeOut;

                await Task.Yield();

                return myTime;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        private async Task<int> GetDuration(string punchinString, string punchOutString, int mealDeduction)
        {

            
            int minutes = 0;
            string timeIn, timeOut;
            bool standardToDayLightSaving = false;
            bool dayLightSavingToStandard = false;
            DateTime fromTime;
            DateTime toTime;

            try
            {
                timeIn = ReverseDate(punchinString) + " " + ReverseTranslate24hr(punchinString);
                fromTime = DateTime.Parse(timeIn);
                timeOut = ReverseDate(punchOutString) + " " + ReverseTranslate24hr(punchOutString);
                toTime = DateTime.Parse(timeOut);
                if (timeIn.Trim() != "" && timeOut.Trim() != "")
                {
                    minutes = (int) (toTime-fromTime).TotalMinutes;
                    DateTime mst = toTime;

                    TimeAndAttendanceSetup setup = await GetTimeAndAttendanceTimeZone();

                    TimeZoneInfo localTime = TimeZoneInfo.FindSystemTimeZoneById(setup.TimeZone); //TimeZoneInfo.Local.StandardName

                    TimeSpan offset = localTime.GetUtcOffset(mst);

                    mst = DateTime.Now.ToUniversalTime().AddHours(offset.Hours).ToUniversalTime();

                    if (localTime.IsDaylightSavingTime(fromTime) == true &&
                        localTime.IsDaylightSavingTime(toTime) == false)
                    {
                        dayLightSavingToStandard = true;
                    }

                    if (localTime.IsDaylightSavingTime(fromTime) == false &&
                        localTime.IsDaylightSavingTime(toTime) == true)
                    {
                        standardToDayLightSaving = true;
                    }

                    if (dayLightSavingToStandard) { minutes += 60; }

                    if (standardToDayLightSaving) { minutes += 60; }

                }

                minutes = minutes - mealDeduction;

                return (minutes);
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }

        public async Task<TimeAndAttendancePunchInView> GetPunchInByIdView(long timePunchinId)
        {
            try
            {
                TimeAndAttendancePunchIn item = await GetObjectAsync(timePunchinId);
                TimeAndAttendancePunchInView view = null;

                if (item != null)
                {
                    view = applicationViewFactory.MapTAPunchinView(item);
                }
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        public async Task<TimeAndAttendancePunchInView> GetPunchOpenView(long employeeId)
        {
            Udc taskStatusQuery = await GetUdc("TA_STATUS", TypeOfTAStatus.Open.ToString().ToUpper());


            TimeAndAttendancePunchIn item = await (from e in _dbContext.TimeAndAttendancePunchIn
                                                   where e.EmployeeId == employeeId
                                                   && e.PunchoutDate==null
                                                   && e.TaskStatusXrefId == taskStatusQuery.XrefId
                                                   select e
                                                   
                                ).FirstOrDefaultAsync<TimeAndAttendancePunchIn>();

            TimeAndAttendancePunchInView view = null;

            if (item != null)
            {
                view = applicationViewFactory.MapTAPunchinView(item);
            }
                           
                
            return view;
        }
        public async Task<TimeAndAttendancePunchIn> GetPunchOpen(long employeeId)
        {
            try
            {
                Udc taskStatusQuery = await GetUdc("TA_STATUS", TypeOfTAStatus.Open.ToString().ToUpper());


                TimeAndAttendancePunchIn item = await (from e in _dbContext.TimeAndAttendancePunchIn
                                                       where e.EmployeeId == employeeId
                                                       && e.PunchoutDate == null
                                                       && e.TaskStatusXrefId == taskStatusQuery.XrefId
                                                       select e
                                    ).FirstOrDefaultAsync<TimeAndAttendancePunchIn>();

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<TimeAndAttendancePunchIn> IsPunchOpen(long employeeId, DateTime asOfDate)
        {
            //bool retVal = false;

            try
            {

                Udc taskStatusQuery = await GetUdc("TA_STATUS", TypeOfTAStatus.Open.ToString().ToUpper());


                TimeAndAttendancePunchIn TAPunch = await (from e in _dbContext.TimeAndAttendancePunchIn
                                                             where e.EmployeeId == employeeId
                                                             && e.PunchinDate >= asOfDate
                                                             && e.PunchoutDate<=asOfDate
                                                             && e.TaskStatusXrefId == taskStatusQuery.XrefId
                                                             select e
                                    ).FirstOrDefaultAsync<TimeAndAttendancePunchIn>();

                //retVal = list.Any(e => e.EmployeeId == employeeId);



                return TAPunch;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        private async Task<TimeAndAttendanceSetup> GetTimeAndAttendanceTimeZone()
        {
            try
            {
                return await _dbContext.TimeAndAttendanceSetup.SingleAsync<TimeAndAttendanceSetup>();
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        private async Task<TimeAndAttendancePunchIn> GetTimeAndAttendancePunchIn(long employeeId, string account, DateTime punchinDate, string punchinDateTime, int hoursDuration, int minutesDuration,int mealDurationInMinutes)
        {
            try
            {
                TimeAndAttendancePunchIn retTA = new TimeAndAttendancePunchIn();

                Task<TimeAndAttendanceScheduledToWork> queryTask = (from a in _dbContext.TimeAndAttendanceScheduledToWork
                                                                    where a.EmployeeId == employeeId
                                                                    && a.StartDate == punchinDate
                                                                   
                                                                    select a).FirstOrDefaultAsync<TimeAndAttendanceScheduledToWork>();

                Task<SupervisorEmployees> supQueryTask = (from a in _dbContext.SupervisorEmployees
                                                          where a.EmployeeId == employeeId
                                                          select a).FirstOrDefaultAsync<SupervisorEmployees>();



                Task<Udc> scheduledTimeUDCTask = GetUdc("TIME", TypeOfTimeEnum.scheduled.ToString());
                Task<Udc> nonScheduledTimeUDCTask = GetUdc("TIME", TypeOfTimeEnum.notscheduled.ToString());
                Task<Udc> taskStatusTask = GetUdc("TA_STATUS", TypeOfTAStatus.Open.ToString().ToUpper());
                Task[] tasksArray = new Task[] { taskStatusTask, queryTask, supQueryTask, scheduledTimeUDCTask, nonScheduledTimeUDCTask };

                Task.WaitAll(tasksArray);


                retTA.PunchinDate = punchinDate;
                retTA.PunchinDateTime = punchinDateTime;
                retTA.EmployeeId = employeeId;
                retTA.Account = account;
                int total_minutesDuration = hoursDuration * 60 + minutesDuration;
                retTA.DurationInMinutes = total_minutesDuration;
                retTA.MealDurationInMinutes = mealDurationInMinutes;

                if (taskStatusTask.Result != null)
                {
                    retTA.TaskStatusXrefId = taskStatusTask.Result.XrefId;
                    retTA.TaskStatus = taskStatusTask.Result.KeyCode;

                }

                if (supQueryTask.Result != null)
                {
                    retTA.SupervisorId = supQueryTask.Result.SupervisorId;
                }

                //Not scheduled to work
                if (queryTask.Result == null)
                {
                    retTA.TypeOfTimeUdcXrefId = nonScheduledTimeUDCTask.Result.XrefId;
                    retTA.TypeOfTime = nonScheduledTimeUDCTask.Result.Value;
                    retTA.Note = "Not Scheduled - Walkin";
                    Udc notscheduled_payCodeUDC = await GetUdc("PAYCODE", TypeOfPayEnum.Regular.ToString());
                    Employee employeeQuery = await (from a in _dbContext.Employee
                                                    where a.EmployeeId == employeeId
                                                    select a).FirstOrDefaultAsync<Employee>();

                    retTA.JobCodeXrefId = employeeQuery.JobTitleXrefId;
                    retTA.JobCode = employeeQuery.JobTitleXref.KeyCode;
                    retTA.PayCodeXrefId = notscheduled_payCodeUDC.XrefId;
                    retTA.PayCode = notscheduled_payCodeUDC.KeyCode;
                }
                if (queryTask.Result != null)
                {
                    Udc jobCodeUDC = await GetUdc("JOBCODE", queryTask.Result.JobCode);
                    Udc payCodeUDC = await GetUdc("PAYCODE", queryTask.Result.PayCode);

                    retTA.TypeOfTimeUdcXrefId = scheduledTimeUDCTask.Result.XrefId;
                    retTA.TypeOfTime = scheduledTimeUDCTask.Result.Value;

                    retTA.JobCodeXrefId = jobCodeUDC.XrefId;
                    retTA.JobCode = queryTask.Result.JobCode;
                    retTA.PayCodeXrefId = payCodeUDC.XrefId;
                    retTA.PayCode = payCodeUDC.KeyCode;

                    retTA.ShiftId = queryTask.Result.ShiftId;
                    retTA.Note = "";
                    retTA.ScheduleId = queryTask.Result.ScheduleId;
                    //retTA.ApprovingAddressId
                    //retTA.TransferJobCode
                    //retTA.TransferSupervisorId
                }


                return (retTA);
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<TimeAndAttendancePunchIn> BuildPunchin(long employeeId, string account,DateTime punchDate)
        {
            try
            {

                //TimeAndAttendanceTimeView currentTime = await GetUTCAdjustedTime();

                string punchDateTime = GetPunchDateTime(punchDate);

                TimeAndAttendancePunchIn retTA = await GetTimeAndAttendancePunchIn(employeeId, account, punchDate, punchDateTime, hoursDuration: 0, minutesDuration: 0, mealDurationInMinutes:0);

                return retTA;

            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<TimeAndAttendanceTimeView> GetUTCAdjustedTime()
        {
            try
            {
                TimeAndAttendanceTimeView utcTime = new TimeAndAttendanceTimeView();

                DateTime mst = DateTime.Now;

                TimeZoneInfo localTime;

                TimeAndAttendanceSetup setup = await GetTimeAndAttendanceTimeZone();

                localTime = TimeZoneInfo.FindSystemTimeZoneById(setup.TimeZone); //TimeZoneInfo.Local.StandardName

                TimeSpan offset = localTime.GetUtcOffset(mst);

                //bool isDaylightSaving = localTime.IsDaylightSavingTime(mst);

                mst = DateTime.Now.ToUniversalTime().AddHours(offset.Hours).ToUniversalTime();

                utcTime.PunchDate = mst;

                string year = mst.Year.ToString();
                string month = Utilities.Right("0" + mst.Month.ToString(), 2);
                string day = Utilities.Right("0" + mst.Day.ToString(), 2);
                string hours = Utilities.Right("0" + mst.Hour.ToString(), 2);
                string minutes = Utilities.Right("0" + mst.Minute.ToString(), 2);
                string seconds = Utilities.Right("0" + mst.Second.ToString(), 2);

                utcTime.PunchDateTime = year + month + day + hours + minutes + seconds;
                return (utcTime);
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByIdAndDate(long employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<TimeAndAttendanceView> list = await (from taPunchin in _dbContext.TimeAndAttendancePunchIn
                                                          where taPunchin.PunchinDate >= startDate
                                                          && taPunchin.PunchinDate <= endDate
                                                          && taPunchin.EmployeeId == employeeId

                                                          join udcTypeOfTime in _dbContext.Udc
                                                          on taPunchin.TypeOfTimeUdcXrefId equals udcTypeOfTime.XrefId

                                                          join udcJobCode in _dbContext.Udc
                                                          on taPunchin.JobCodeXrefId equals udcJobCode.XrefId

                                                          join udcPayCode in _dbContext.Udc
                                                          on taPunchin.PayCodeXrefId equals udcPayCode.XrefId into ljPayCode
                                                          from udcPayCode in ljPayCode.DefaultIfEmpty()

                                                          join taSchedule in _dbContext.TimeAndAttendanceSchedule
                                                          on taPunchin.ScheduleId equals taSchedule.ScheduleId

                                                          join taShift in _dbContext.TimeAndAttendanceShift
                                                          on taPunchin.ShiftId equals taShift.ShiftId into ljShift
                                                          from taShift in ljShift.DefaultIfEmpty()

                                                          join supervisor in _dbContext.Supervisor
                                                          on taPunchin.SupervisorId equals supervisor.SupervisorId

                                                          join supervisorAddressBook in _dbContext.AddressBook
                                                          on supervisor.AddressId equals supervisorAddressBook.AddressId

                                                          join employee in _dbContext.Employee
                                                          on taPunchin.EmployeeId equals employee.EmployeeId


                                                          join employeeAddressBook in _dbContext.AddressBook
                                                          on employee.AddressId equals employeeAddressBook.AddressId

                                                          join approverAddressBook in _dbContext.AddressBook
                                                          on taPunchin.ApprovingAddressId equals approverAddressBook.AddressId

                                                          select new TimeAndAttendanceView
                                                          {
                                                              TimePunchinId = taPunchin.TimePunchinId,
                                                              PunchinDate = taPunchin.PunchinDate,
                                                              PunchoutDate = taPunchin.PunchoutDate,
                                                              PunchinDateTime = taPunchin.PunchinDateTime,
                                                              PunchoutDateTime = taPunchin.PunchoutDateTime,
                                                              PayCode = udcPayCode.Value,
                                                              TypeOfTime = udcTypeOfTime.Value,
                                                              JobCode = udcJobCode.Value,
                                                              ApproverAddressId = approverAddressBook.AddressId,
                                                              ApproverName = approverAddressBook.Name,
                                                              EmployeeName = employeeAddressBook.Name,
                                                              EmployeeId = employee.EmployeeId,
                                                              ShiftId = (long?)taShift.ShiftId,
                                                              ShiftName = taShift.ShiftName,
                                                              ShiftType = taShift.ShiftType,
                                                              ScheduleId = taSchedule.ScheduleId,
                                                              ScheduleName = taSchedule.ScheduleName,
                                                              ScheduleStartDate = taSchedule.StartDate,
                                                              ScheduleEndDate = taSchedule.EndDate,
                                                              ScheduleGroup = taSchedule.ScheduleGroup,
                                                              Monday = taSchedule.Monday,
                                                              Tuesday = taSchedule.Tuesday,
                                                              Wednesday = taSchedule.Wednesday,
                                                              Thursday = taSchedule.Thursday,
                                                              Friday = taSchedule.Friday,
                                                              Saturday = taSchedule.Saturday,
                                                              Sunday = taSchedule.Sunday,
                                                              ScheduledToWork = _dbContext.TimeAndAttendanceScheduledToWork.Any(e => e.EmployeeId == employee.EmployeeId && e.ScheduleId == taSchedule.ScheduleId)
                                                          }).ToListAsync<TimeAndAttendanceView>();

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<List<TimeAndAttendanceView>> GetTimeAndAttendanceViewsByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<TimeAndAttendanceView> list = await (from taPunchin in _dbContext.TimeAndAttendancePunchIn
                                                          where taPunchin.PunchinDate >= startDate
                                                          && taPunchin.PunchinDate <= endDate

                                                          join udcTypeOfTime in _dbContext.Udc
                                                          on taPunchin.TypeOfTimeUdcXrefId equals udcTypeOfTime.XrefId

                                                          join udcJobCode in _dbContext.Udc
                                                          on taPunchin.JobCodeXrefId equals udcJobCode.XrefId

                                                          join udcPayCode in _dbContext.Udc
                                                          on taPunchin.PayCodeXrefId equals udcPayCode.XrefId into ljPayCode
                                                          from udcPayCode in ljPayCode.DefaultIfEmpty()

                                                          join taSchedule in _dbContext.TimeAndAttendanceSchedule
                                                          on taPunchin.ScheduleId equals taSchedule.ScheduleId

                                                          join taShift in _dbContext.TimeAndAttendanceShift
                                                          on taPunchin.ShiftId equals taShift.ShiftId into ljShift
                                                          from taShift in ljShift.DefaultIfEmpty()

                                                          join supervisor in _dbContext.Supervisor
                                                          on taPunchin.SupervisorId equals supervisor.SupervisorId

                                                          join supervisorAddressBook in _dbContext.AddressBook
                                                          on supervisor.AddressId equals supervisorAddressBook.AddressId

                                                          join employee in _dbContext.Employee
                                                          on taPunchin.EmployeeId equals employee.EmployeeId


                                                          join employeeAddressBook in _dbContext.AddressBook
                                                          on employee.AddressId equals employeeAddressBook.AddressId

                                                          join approverAddressBook in _dbContext.AddressBook
                                                          on taPunchin.ApprovingAddressId equals approverAddressBook.AddressId

                                                          select new TimeAndAttendanceView
                                                          {
                                                              TimePunchinId = taPunchin.TimePunchinId,
                                                              PunchinDate = taPunchin.PunchinDate,
                                                              PunchoutDate = taPunchin.PunchoutDate,
                                                              PunchinDateTime = taPunchin.PunchinDateTime,
                                                              PunchoutDateTime = taPunchin.PunchoutDateTime,
                                                              PayCode = udcPayCode.Value,
                                                              TypeOfTime = udcTypeOfTime.Value,
                                                              JobCode = udcJobCode.Value,
                                                              ApproverAddressId = approverAddressBook.AddressId,
                                                              ApproverName = approverAddressBook.Name,
                                                              EmployeeName = employeeAddressBook.Name,
                                                              EmployeeId = employee.EmployeeId,
                                                              ShiftId = (long?)taShift.ShiftId,
                                                              ShiftName = taShift.ShiftName,
                                                              ShiftType = taShift.ShiftType,
                                                              ScheduleId = taSchedule.ScheduleId,
                                                              ScheduleName = taSchedule.ScheduleName,
                                                              ScheduleStartDate = taSchedule.StartDate,
                                                              ScheduleEndDate = taSchedule.EndDate,
                                                              ScheduleGroup = taSchedule.ScheduleGroup,
                                                              ScheduledToWork = _dbContext.TimeAndAttendanceScheduledToWork.Any(e => e.EmployeeId == employee.EmployeeId && e.ScheduleId == taSchedule.ScheduleId)
                                                          }).ToListAsync<TimeAndAttendanceView>();


                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<TimeAndAttendanceShift> GetShiftById(long shiftId)
        {
            try
            {
                var query = await (from a in _dbContext.TimeAndAttendanceShift
                                   where a.ShiftId == shiftId
                                   select a).FirstOrDefaultAsync<TimeAndAttendanceShift>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<List<TimeAndAttendancePunchInView>> GetTAPunchinByEmployeeId(long employeeId)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.EmployeeId == employeeId, "").ToListAsync();

                List<TimeAndAttendancePunchInView> listView = new List<TimeAndAttendancePunchInView>();

                foreach (var item in list)
                {
                    listView.Add(applicationViewFactory.MapTAPunchinView(item));
                }


                return listView;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public CreateProcessStatus DeletePunchin(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                DeleteObject(taPunchin);
                return CreateProcessStatus.Delete;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        private double GetHours(int duration)
        {
            double retVal = 0;
            retVal = duration / 60;
            return (retVal);
        }
    

        public async Task<CreateProcessStatus> UpdatePunchin(TimeAndAttendancePunchIn taPunchin,int mealDeduction,int manualElapsedHours= 0, int manualElapsedMinutes = 0)
        {

            try
            {
                //long timePunchinId = 0;
                long? employeeId = taPunchin.EmployeeId;
                string punchinDateTime = taPunchin.PunchinDateTime;
                int minutesDuration = 0;
                TimeAndAttendanceTimeView currentTime = null;
                //Case 1 : Manual entry of punchout time based on minutes duration
                if (manualElapsedHours != 0 || manualElapsedMinutes != 0)
                {
                    minutesDuration = manualElapsedHours * 60 + manualElapsedMinutes - mealDeduction;
                    currentTime = await GetTimeByMinuteDuration(punchinDateTime, minutesDuration);
                }
                //Case 2 : User presses punchout
                else
                {
                    currentTime= await GetUTCAdjustedTime();
                    minutesDuration = await GetDuration(punchinDateTime, currentTime.PunchDateTime, mealDeduction);
                }
                taPunchin.PunchoutDate = currentTime.PunchDate;
                taPunchin.PunchoutDateTime = currentTime.PunchDateTime;

                taPunchin.DurationInMinutes = minutesDuration;
                taPunchin.MealDurationInMinutes = mealDeduction;

                Udc status = await GetUdc("TA_STATUS", TypeOfTAStatus.Closed.ToString().ToUpper());
               
                taPunchin.TaskStatusXrefId = status.XrefId;
                taPunchin.TaskStatus = status.KeyCode;

                UpdateObject(taPunchin);
                return CreateProcessStatus.Update;
                      
               
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }       
           
            
        }
        public async Task<CreateProcessStatus> AddPunchin(TimeAndAttendancePunchIn taPunchin)
        {
            try
            {
                //long timePunchinId = 0;
                long? employeeId = taPunchin.EmployeeId;
                string punchinDateTime = taPunchin.PunchinDateTime;

                var query = await (from a in _dbContext.TimeAndAttendancePunchIn
                                   where a.EmployeeId == employeeId
                                   && a.PunchinDateTime == punchinDateTime
                                   select a).FirstOrDefaultAsync<TimeAndAttendancePunchIn>();
                if (query == null)
                {
                    AddObject(taPunchin);
                    return CreateProcessStatus.Insert;
                    //_dbContext.SaveChanges();
                    //timePunchinId = taPunchin.TimePunchinId;
                }


                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<CreateProcessStatus> UpdateByTimePunchinId(long timePunchinId, int workDurationInMinutes, int mealDurationInMinutes)
        {
            try
            {
                var query = await GetObjectAsync(timePunchinId);

                TimeAndAttendancePunchIn taPunchin = query;
                taPunchin.DurationInMinutes = workDurationInMinutes;
                taPunchin.MealDurationInMinutes = mealDurationInMinutes;

                string punchoutDateTime = BuildPunchOut(taPunchin.PunchinDateTime, workDurationInMinutes);
                DateTime punchoutDate = GetPunchDateTime(punchoutDateTime);

                taPunchin.PunchoutDateTime = punchoutDateTime;
                taPunchin.PunchoutDate = punchoutDate;

                UpdateObject(taPunchin);
                return CreateProcessStatus.Update;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public string GetPunchDateTime(DateTime? myDate)
        {
            try
            {
                String year = "", month = "", day = "";
                String longHours = "", minutes = "", seconds = "";

                if (myDate != null)

                {
                    year = myDate?.Year.ToString();
                    month = myDate?.Month.ToString().PadLeft(2, '0');
                    day = myDate?.Day.ToString().PadLeft(2, '0');

                    longHours = myDate?.Hour.ToString().PadLeft(2, '0');
                    minutes = myDate?.Minute.ToString().PadLeft(2, '0');
                    seconds = myDate?.Second.ToString().PadLeft(2, '0');
                }
                return year + month + day + longHours + minutes + seconds;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
        public DateTime GetPunchDateTime(string s24Hrs)
        {
            try
            {

                String year, month, day = "";
                String longHours, minutes, seconds = "";
                DateTime myDate;


                month = s24Hrs.Substring(4, 2);
                day = s24Hrs.Substring(6, 2);
                year = s24Hrs.Substring(0, 4);

                longHours = s24Hrs.Substring(8, 2);
                minutes = s24Hrs.Substring(10, 2);
                seconds = s24Hrs.Substring(12, 2);

                myDate = Convert.ToDateTime(month + "/" + day + "/" + year + " " + longHours + ":" + minutes + ":" + seconds);

                return myDate;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        String BuildPunchOut(string s24Hrs, int durationInMinutes)
        {
            try
            {

                String year, month, day = "";
                String longHours, minutes, seconds = "";
                DateTime myDate;

                myDate = GetPunchDateTime(s24Hrs);
                myDate = myDate.AddMinutes(durationInMinutes);


                year = myDate.Year.ToString();
                month = myDate.Month.ToString().PadLeft(2, '0');
                day = myDate.Day.ToString().PadLeft(2, '0');

                longHours = myDate.Hour.ToString().PadLeft(2, '0');
                minutes = myDate.Minute.ToString().PadLeft(2, '0');
                seconds = myDate.Second.ToString().PadLeft(2, '0');

                return year + month + day + longHours + minutes + seconds;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }
    }
}
