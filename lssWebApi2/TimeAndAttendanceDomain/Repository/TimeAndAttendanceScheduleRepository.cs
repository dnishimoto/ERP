using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.TimeAndAttendanceDomain
{
    public class TimeAndAttendanceScheduleView: AbstractModule
    {
        public TimeAndAttendanceScheduleView()
        { }
        public TimeAndAttendanceScheduleView(TimeAndAttendanceSchedule schedule)
        {
            this.ScheduleId = schedule.ScheduleId;
            this.ScheduleName = schedule.ScheduleName;
            this.StartDate = schedule.StartDate;
            this.EndDate = schedule.EndDate;
            this.ShiftId = schedule.ShiftId;
            this.ShiftName = schedule.Shift.ShiftName;
            this.ShiftStartTime = schedule.Shift.ShiftStartTime;
            this.ShiftEndTime = schedule.Shift.ShiftEndTime;
            this.ScheduleGroup = schedule.ScheduleGroup;
            this.StartDateTime = BuildLongDate(this.StartDate??DateTime.Now, this.ShiftStartTime??0);
            this.EndDateTime = BuildLongDate(this.EndDate??DateTime.Now, this.ShiftEndTime??0);
        }

        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int? ShiftStartTime { get; set; }
        public int? ShiftEndTime { get; set; }
        public string ScheduleGroup { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }

        String BuildLongDate(DateTime myDate, int hours)
        {
            try
            {

                String year, month, day = "";
                string myLongTime = "0" + hours+"00"; myLongTime = myLongTime.Substring(myLongTime.Length - 6);
                
                year = myDate.Year.ToString();
                month = myDate.Month.ToString().PadLeft(2, '0');
                day = myDate.Day.ToString().PadLeft(2, '0');

                //longHours = myDate.Hour.ToString().PadLeft(2, '0');
                //minutes = myDate.Minute.ToString().PadLeft(2, '0');
                //seconds = myDate.Second.ToString().PadLeft(2, '0');
                return year + month + day + myLongTime;
                //return year + month + day + longHours + minutes + seconds;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }

   
    public class TimeAndAttendanceScheduleRepository : Repository<TimeAndAttendanceSchedule>
    {
        public ListensoftwareDBContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceScheduleRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public CreateProcessStatus DeleteSchedule(TimeAndAttendanceSchedule schedule)
        {
            try
            {
                DeleteObject(schedule);
                return CreateProcessStatus.Delete;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public TimeAndAttendanceScheduleView BuildTimeAndAttendanceScheduleView(TimeAndAttendanceSchedule item)
        {
            TimeAndAttendanceScheduleView view = applicationViewFactory.MapTimeAndAttendanceScheduleView(item);
            return view;
        }
        public async Task<CreateProcessStatus> AddSchedule(TimeAndAttendanceScheduleView view)
        {
            try
            {
                //long timePunchinId = 0;
                var query = await (from a in _dbContext.TimeAndAttendanceSchedule
                                   where a.ScheduleName == view.ScheduleName
                               && a.StartDate == view.StartDate
                               && a.EndDate == view.EndDate
                               && a.ScheduleGroup == view.ScheduleGroup
                               && a.ShiftId == view.ShiftId

                                   select a).FirstOrDefaultAsync<TimeAndAttendanceSchedule>();

                TimeAndAttendanceSchedule schedule = new TimeAndAttendanceSchedule();
                applicationViewFactory.MapTimeAndAttendanceScheduleEntity(ref schedule, view);
                if (query == null)
                {
                    

                    AddObject(schedule);
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
    }
}
