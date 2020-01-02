using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.TimeAndAttendanceDomain;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace lssWebApi2.TimeAndAttendanceScheduleDomain
{
    public class TimeAndAttendanceScheduleDayView
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }

    }
    public class TimeAndAttendanceScheduleView : AbstractModule
    {
     
        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string ShiftStartTime { get; set; }
        public string ShiftEndTime { get; set; }
        public string ScheduleGroup { get; set; }
        public int? DurationHours { get; set; }
        public int? DurationMinutes { get; set; }
        public bool? Monday{ get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }

        public long TimeAndAttendanceScheduleNumber { get; set; }


        //public string StartDateTime { get; set; }
        // public string EndDateTime { get; set; }


    }

   
    public class TimeAndAttendanceScheduleRepository : Repository<TimeAndAttendanceSchedule>, ITimeAndAttendanceScheduleRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceScheduleRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public IQueryable<TimeAndAttendanceSchedule> GetEntitiesByExpression(Expression<Func<TimeAndAttendanceSchedule, bool>> predicate)
         {
            var result =  _dbContext.Set<TimeAndAttendanceSchedule>().Where(predicate);

            return result;
        }
        public async Task<TimeAndAttendanceSchedule> GetEntityById(long? timePunchinId)
        {
            return await _dbContext.TimeAndAttendanceSchedule.FindAsync(timePunchinId);
        }
        public async Task<TimeAndAttendanceSchedule> GetEntityByNumber(long scheduleNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.TimeAndAttendanceSchedule
                                   where detail.TimeAndAttendanceScheduleNumber == scheduleNumber
                                   select detail).FirstOrDefaultAsync<TimeAndAttendanceSchedule>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }


    }
}
