using lssWebApi2.AbstractFactory;
using lssWebApi2.EmployeeDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lssWebApi2;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.TimeAndAttendanceScheduleDomain;

namespace lssWebApi2.TimeAndAttendanceScheduledToWorkDomain
{
    public class TimeAndAttendanceScheduledToWorkView
    {
            public long? ScheduledToWorkId { get; set; }
            public long ? EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public long ? ScheduleId { get; set; }
            public string ScheduleName { get; set; }
            public DateTime? ScheduleStartDate { get; set; }
            public DateTime? ScheduleEndDate { get; set; }
            public long ? ShiftId { get; set; }
            public string ShiftStartTime { get; set; }
            public string ShiftEndTime { get; set; }
            public int DurationHours { get; set; }
            public int DurationMinutes { get; set; }
            public bool? Monday { get; set; }
            public bool? Tuesday { get; set; }
            public bool? Wednesday { get; set; }
            public bool? Thursday { get; set; }
            public bool? Friday { get; set; }
            public bool? Saturday { get; set; }
            public bool? Sunday { get; set; }
            public long? JobCodeXrefId { get; set; }
            public long? PayCodeXrefId { get; set; }
            public long? WorkedJobCodeXrefId { get; set; }
            public string PayCode { get; set; }
            public string JobCode { get; set; }
            public string WorkedJobCode { get; set; }
            public long ScheduledToWorkNumber { get; set; }
            public string Note { get; set; }

    }
    public class TimeAndAttendanceScheduledToWorkRepository : Repository<TimeAndAttendanceScheduledToWork>, ITimeAndAttendanceScheduledToWorkRepository
    {
        public ListensoftwaredbContext _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceScheduledToWorkRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<TimeAndAttendanceScheduledToWork> GetEntityById(long? scheduledToWorkId)
        {
            return await _dbContext.TimeAndAttendanceScheduledToWork.FindAsync(scheduledToWorkId);
        }
        public async Task<TimeAndAttendanceScheduledToWork> GetEntityByNumber(long scheduledToWorkNumber)
        {
            try
            {
                var query = await (from detail in _dbContext.TimeAndAttendanceScheduledToWork
                                   where detail.ScheduledToWorkNumber == scheduledToWorkNumber
                                   select detail).FirstOrDefaultAsync<TimeAndAttendanceScheduledToWork>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public TimeAndAttendanceScheduledToWork BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, TimeAndAttendanceScheduleDayView dayView, EmployeeView employeeItem,string payCode)
        {
            TimeAndAttendanceScheduledToWork scheduledToWork = new TimeAndAttendanceScheduledToWork();

            applicationViewFactory.MapTimeAndAttendanceScheduledToWorkEntity(ref scheduledToWork, scheduleView, dayView, employeeItem,payCode);

            return scheduledToWork;
        }

        public async Task<CreateProcessStatus> AddScheduledToWorkItems(IList<TimeAndAttendanceScheduledToWork> items)
        {
            try
            {
                //long timePunchinId = 0;
                List<TimeAndAttendanceScheduledToWork> list = new List<TimeAndAttendanceScheduledToWork>();
                foreach (var item in items)
                {
                    var query = await (from a in _dbContext.TimeAndAttendanceScheduledToWork
                                       where a.ScheduleId == item.ScheduleId
                                   && a.EmployeeId == item.EmployeeId


                                       select a).FirstOrDefaultAsync<TimeAndAttendanceScheduledToWork>();

                    if (query == null)
                    {
                        //TimeAndAttendanceScheduledToWork scheduledToWork = new TimeAndAttendanceScheduledToWork();
                        //scheduledToWork.EmployeeId = item.EmployeeId;
                        //scheduledToWork.ScheduleId = item.ScheduleId;
                        list.Add(item);
                    }

                }

                if (list.Count > 0)
                {
                    AddObjects(list);
                    return CreateProcessStatus.Insert;

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
