using ERP_Core2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillenniumERP.Services;
using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;

namespace MillenniumERP.TimeAndAttendanceDomain
{
    public class TimeAndAttendanceScheduleView
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
            this.ShiftName = schedule.TimeAndAttendanceShift.ShiftName;
            this.ShiftStartTime = schedule.TimeAndAttendanceShift.ShiftStartTime;
            this.ShiftEndTime = schedule.TimeAndAttendanceShift.ShiftEndTime;
            this.ScheduleGroup = schedule.ScheduleGroup;

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

    }

    public class TimeAndAttendanceScheduleRepository : Repository<TimeAndAttendanceSchedule>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceScheduleRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
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
        public async Task<CreateProcessStatus> AddSchedule(TimeAndAttendanceScheduleView view)
        {
            try
            {
                //long timePunchinId = 0;
                var query = await (from a in _dbContext.TimeAndAttendanceSchedules
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
