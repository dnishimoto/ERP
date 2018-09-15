using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using ERP_Core2.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.TimeAndAttendanceDomain.Repository
{
    public class TimeAndAttendanceScheduledToWorkView
    {
        public TimeAndAttendanceScheduledToWorkView() { }
        public TimeAndAttendanceScheduledToWorkView(TimeAndAttendanceScheduledToWork scheduledToWork)
        {
            this.EmployeeId = scheduledToWork.EmployeeId;
            this.EmployeeName = scheduledToWork.Employee.AddressBook.Name;
            this.ScheduleId = scheduledToWork.ScheduleId;
            this.ScheduleName = scheduledToWork.TimeAndAttendanceSchedule.ScheduleName;
            this.ScheduleStartDate = scheduledToWork.TimeAndAttendanceSchedule.StartDate;
            this.ScheduleEndDate = scheduledToWork.TimeAndAttendanceSchedule.EndDate;
            this.ShiftStartTime = scheduledToWork.TimeAndAttendanceSchedule.TimeAndAttendanceShift.ShiftStartTime;
            this.ShiftEndTime = scheduledToWork.TimeAndAttendanceSchedule.TimeAndAttendanceShift.ShiftEndTime;
        }
     
            public long EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public long ScheduleId { get; set; }
            public string ScheduleName { get; set; }
            public DateTime? ScheduleStartDate { get; set; }
            public DateTime? ScheduleEndDate { get; set; }
            public int? ShiftStartTime { get; set; }
            public int? ShiftEndTime { get; set; }

    }
    public class TimeAndAttendanceScheduledToWorkRepository : Repository<TimeAndAttendanceScheduledToWork>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceScheduledToWorkRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public TimeAndAttendanceScheduledToWork BuildScheduledToWork(TimeAndAttendanceScheduleView scheduleView, EmployeeView employeeItem)
        {
            TimeAndAttendanceScheduledToWork scheduledToWork = new TimeAndAttendanceScheduledToWork();

            applicationViewFactory.MapTimeAndAttendanceScheduledToWorkEntity(ref scheduledToWork, scheduleView, employeeItem);

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
                    var query = await (from a in _dbContext.TimeAndAttendanceScheduledToWorks
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
