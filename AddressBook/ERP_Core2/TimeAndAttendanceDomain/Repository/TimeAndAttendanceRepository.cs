﻿
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;

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
            this.EmployeeName = taPunchin.Employee.AddressBook.Name;
            this.SupervisorId = taPunchin.SupervisorId;
            this.SupervisorName = taPunchin.Supervisor.AddressBook.Name;
            this.ProcessedDate = taPunchin.ProcessedDate;
            this.Note = taPunchin.Note;
            this.ShiftId = taPunchin.ShiftId;
            this.ScheduledToWork = taPunchin.ScheduledToWork;
            this.TypeOfTimeUdcXrefId = taPunchin.TypeOfTimeUdcXrefId;
            this.TypeOfTime = taPunchin.UDC.Value;
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

    public class TimeAndAttendanceRepository : Repository<TimeAndAttendancePunchIn>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<TimeAndAttendanceShift> GetShiftById(long shiftId)
        {
            try
            {
                var query = await (from a in _dbContext.TimeAndAttendanceShifts
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

                var query = await (from a in _dbContext.TimeAndAttendancePunchIns
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
