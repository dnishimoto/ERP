using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.Services;
using ERP_Core2.AbstractFactory;

namespace MillenniumERP.ScheduleEventsDomain
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

    public class TimeAndAttendanceRepository: Repository<TimeAndAttendancePunchIn>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<List<TimeAndAttendancePunchInView>> GetTAPunchinByEmployeeId(int employeeId)
        {
           
            var list = await base.GetObjectsAsync(e => e.EmployeeId == employeeId, "").ToListAsync();

            List<TimeAndAttendancePunchInView> listView=new List<TimeAndAttendancePunchInView>();

            foreach (var item in list)
            {
                listView.Add(applicationViewFactory.MapTAPunchinView(item));
            }


            return listView;
           
        }
        public async Task<bool> AddPunchin(TimeAndAttendancePunchIn taPunchin)
        {
            long? employeeId = taPunchin.EmployeeId;
            string punchinDateTime = taPunchin.PunchinDateTime;

            var query = (from a in _dbContext.TimeAndAttendancePunchIns
                         where a.EmployeeId == employeeId
                         && a.PunchinDateTime == punchinDateTime
                         select a).FirstOrDefault<TimeAndAttendancePunchIn>();
            if (query == null)
            {
                AddObject(taPunchin);
            }

            
            return true;
        }
        public async Task<bool> UpdateByTimePunchinId(long? timePunchinId, int workDurationInMinutes,int mealDurationInMinutes)
        {
            try
            {
                var query = GetObjectAsync((int)timePunchinId);

                TimeAndAttendancePunchIn taPunchin = query.Result;
                taPunchin.DurationInMinutes = workDurationInMinutes;
                taPunchin.MealDurationInMinutes = mealDurationInMinutes;

                string punchoutDateTime = BuildPunchOut(taPunchin.PunchinDateTime, workDurationInMinutes);
                DateTime punchoutDate = GetPunchDateTime(punchoutDateTime);

                taPunchin.PunchoutDateTime = punchoutDateTime;
                taPunchin.PunchoutDate = punchoutDate;

                UpdateObject(taPunchin);
                return true;
            }
            catch (Exception ex)
            {
                
            }
            return false;
            }
        public string GetPunchDateTime(DateTime? myDate)
        {
            String year="", month="", day = "";
            String longHours="", minutes="", seconds = "";

            if (myDate != null)

            {
                year = myDate?.Year.ToString();
                month = myDate?.Month.ToString().PadLeft(2, '0');
                day = myDate?.Day.ToString().PadLeft(2, '0');

                longHours = myDate?.Hour.ToString().PadLeft(2,'0');
                minutes = myDate?.Minute.ToString().PadLeft(2, '0');
                seconds = myDate?.Second.ToString().PadLeft(2, '0');
            }
            return year + month + day + longHours + minutes + seconds;
        }
        public DateTime GetPunchDateTime(string s24Hrs)
        {
            DateTime dDate;
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
        String BuildPunchOut(string s24Hrs, int durationInMinutes)
        {
            DateTime dDate;
            String year, month, day = "";
            String longHours, minutes, seconds = "";
            DateTime myDate;

            myDate = GetPunchDateTime(s24Hrs);
            myDate = myDate.AddMinutes(durationInMinutes);


            year = myDate.Year.ToString();
            month = myDate.Month.ToString().PadLeft(2, '0');
            day = myDate.Day.ToString().PadLeft(2, '0');

            longHours = myDate.Hour.ToString().PadLeft(2,'0');
            minutes = myDate.Minute.ToString().PadLeft(2, '0');
            seconds = myDate.Second.ToString().PadLeft(2, '0');

            return year + month + day + longHours + minutes + seconds;
        }
    }
}
