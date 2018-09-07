using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.EntityFramework;
using MillenniumERP.AddressBookDomain;
using MillenniumERP.Services;
using MillenniumERP.TimeAndAttendanceDomain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.TimeAndAttendanceDomain.Repository
{
    class TimeAndAttendanceScheduledToWorkRepository : Repository<TimeAndAttendanceScheduledToWork>
    {
        public Entities _dbContext;
        private ApplicationViewFactory applicationViewFactory;
        public TimeAndAttendanceScheduledToWorkRepository(DbContext db) : base(db)
        {
            _dbContext = (Entities)db;
            applicationViewFactory = new ApplicationViewFactory();
        }

        public async Task<CreateProcessStatus> AddScheduledToWork(TimeAndAttendanceScheduleView view, List<EmployeeView> employeeViews)
        {
            try
            {
                //long timePunchinId = 0;
                List<TimeAndAttendanceScheduledToWork> list = new List<TimeAndAttendanceScheduledToWork>();
                foreach (var item in employeeViews)
                {
                    var query = await (from a in _dbContext.TimeAndAttendanceScheduledToWorks
                                       where a.ScheduleId == view.ScheduleId
                                   && a.EmployeeId == item.EmployeeId


                                       select a).FirstOrDefaultAsync<TimeAndAttendanceScheduledToWork>();

                    if (query == null)
                    {
                        TimeAndAttendanceScheduledToWork scheduledToWork = new TimeAndAttendanceScheduledToWork();
                        scheduledToWork.EmployeeId = item.EmployeeId ?? 0;
                        scheduledToWork.ScheduleId = view.ScheduleId;
                        list.Add(scheduledToWork);
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
