using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using lssWebApi2.Mapper;

using lssWebApi2.EntityFramework;
using X.PagedList;


namespace ERP_Core2.TimeAndAttendanceDomain
{ 

    public class TimeAndAttendancePunchInView
    {
        public TimeAndAttendancePunchInView(TimeAndAttendancePunchIn taPunchin)
        {
            this.TimePunchinId = taPunchin.TimePunchinId;
            this.PunchinDate = taPunchin.PunchinDate;
            this.PunchinDateTime = taPunchin.PunchinDateTime;
            this.PunchoutDateTime = taPunchin.PunchoutDateTime;
            this.JobCodeXrefId = taPunchin.JobCodeXrefId;
            //this.JobCode = taPunchin.
            this.Approved = taPunchin.Approved;
            this.EmployeeId = taPunchin.EmployeeId;
            this.EmployeeName = taPunchin.Employee.Address.Name;
            this.SupervisorId = taPunchin.SupervisorId;
            this.SupervisorName = taPunchin.SupervisorNavigation.Address.Name;
            this.ProcessedDate = taPunchin.ProcessedDate;
            this.Note = taPunchin.Note;
            this.ShiftId = taPunchin.ShiftId;
            this.ScheduledToWork = taPunchin.ScheduledToWork;
            this.TypeOfTimeUdcXrefId = taPunchin.TypeOfTimeUdcXrefId;
            this.TypeOfTime = taPunchin.TypeOfTimeUdcXref.Value;
            this.ApprovingAddressId = taPunchin.ApprovingAddressId;
            this.PayCodeXrefId = taPunchin.PayCodeXrefId;
            //this.PayCode = taPunchin.
            this.ScheduleId = taPunchin.ScheduleId;
            this.DurationInMinutes = taPunchin.DurationInMinutes;
            this.MealDurationInMinutes = taPunchin.MealDurationInMinutes;
        }

        public long? TimePunchinId { get; set; }
        public DateTime? PunchinDate { get; set; }
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

    }

    public class TimeAndAttendanceView
    {
        public long TimePunchinId { get; set; }
        public DateTime ? PunchinDate { get; set; }
        public DateTime ? PunchoutDate { get; set; }
        public string PunchinDateTime { get; set; }
        public string PunchoutDateTime { get; set; }
        public string PayCode { get; set; }
        public string TypeOfTime { get; set; }
        public string JobCode { get; set; }
        public long ApproverAddressId { get; set; }
        public string ApproverName { get; set; }
        public string EmployeeName { get; set; }
        public long EmployeeId { get; set; }
        public long ? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string ShiftType { get; set; }
        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime ? ScheduleStartDate { get; set; }
        public DateTime ? ScheduleEndDate { get; set; }
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
        public DateTime PunchinDate { get; set; }
        public String PunchinDateTime { get; set; }
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

    public class TimeAndAttendanceRepository : Repository<TimeAndAttendancePunchIn>
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<IPagedList<TimeAndAttendancePunchIn>> GetTimeAndAttendanceViewsByPage(Func<TimeAndAttendancePunchIn, bool> predicate, Func<TimeAndAttendancePunchIn, object> order, int pageSize, int pageNumber)
        {
            IEnumerable<TimeAndAttendancePunchIn> query = _dbContext.TimeAndAttendancePunchIn
                          .Where(predicate).OrderBy(order).Select(e => e);

            IPagedList<TimeAndAttendancePunchIn> list = await query.ToPagedListAsync(pageNumber, pageSize);
            return list;

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
        public async Task<TimeAndAttendancePunchIn> BuildPunchin(long employeeId)
        {

            TimeAndAttendancePunchIn retTA = new TimeAndAttendancePunchIn();
            TimeAndAttendanceTimeView currentTime = await GetUTCAdjustedTime();
            retTA.PunchinDate = currentTime.PunchinDate;
            retTA.PunchinDateTime = currentTime.PunchinDateTime;
            retTA.EmployeeId = employeeId;

        
            /*
            retTA.JobCodeXrefId
            
            retTA.SupervisorId 
            retTA.Note 
            retTA.ShiftId 
         retTA.ScheduledToWork 
    retTA.TypeOfTimeUdcXrefId 
retTA.ApprovingAddressId 
         retTA.PayCodeXrefId
         retTA.ScheduleId 
          retTA.TypeOfTime 
retTA.PayCode 
         retTA.JobCode 
         retTA.TransferJobCode 
         retTA.TransferSupervisorId 
         */
            return (retTA);
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

                utcTime.PunchinDate = mst;

                string year = mst.Year.ToString();
                string month = Utilities.Right("0" + mst.Month.ToString(), 2);
                string day = Utilities.Right("0" + mst.Day.ToString(), 2);
                string hours = Utilities.Right("0" + mst.Hour.ToString(), 2);
                string minutes = Utilities.Right("0" + mst.Minute.ToString(), 2);
                string seconds = Utilities.Right("0" + mst.Second.ToString(), 2);

                utcTime.PunchinDateTime = year + month + day + hours + minutes + seconds;
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
                                                              Wednesday=taSchedule.Wednesday,
                                                              Thursday=taSchedule.Thursday,
                                                              Friday=taSchedule.Friday,
                                                              Saturday=taSchedule.Saturday,
                                                              Sunday=taSchedule.Sunday,
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
               List< TimeAndAttendanceView> list = await (from taPunchin in _dbContext.TimeAndAttendancePunchIn
                             where taPunchin.PunchinDate >= startDate
                             && taPunchin.PunchinDate<= endDate

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
