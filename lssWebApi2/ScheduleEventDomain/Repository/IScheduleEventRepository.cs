

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.ScheduleEventDomain
{
public interface IScheduleEventRepository
    {
        Task<ScheduleEvent> GetEntityById(long ? scheduleEventId);
        Task<IList<ScheduleEvent>> GetEntitiesByCustomerId(long ? customerId);
        Task<IList<ScheduleEvent>> GetEntitiesByServiceId(long? serviceId);
        Task<IQueryable<ScheduleEvent>> GetEntitiesByEmployeeId(long ? employeeId);
    }
}
