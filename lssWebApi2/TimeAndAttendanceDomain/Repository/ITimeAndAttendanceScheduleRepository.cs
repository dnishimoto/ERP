using ERP_Core2.AccountPayableDomain;
using ERP_Core2.TimeAndAttendanceDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TimeAndAttendanceDomain
{
    public interface ITimeAndAttendanceScheduleRepository
    {
        CreateProcessStatus DeleteSchedule(TimeAndAttendanceSchedule schedule);
        TimeAndAttendanceScheduleView BuildTimeAndAttendanceScheduleView(TimeAndAttendanceSchedule item);
        Task<CreateProcessStatus> AddSchedule(TimeAndAttendanceScheduleView view);
    }
}
