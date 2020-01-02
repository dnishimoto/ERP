using lssWebApi2.AutoMapper;
using lssWebApi2.ScheduleEventDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public interface IFluentScheduleEventQuery
{
    Task<ScheduleEvent> MapToEntity(ScheduleEventView inputObject);
    Task<List<ScheduleEvent>> MapToEntity(List<ScheduleEventView> inputObjects);
    Task<ScheduleEventView> MapToView(ScheduleEvent inputObject);
    Task<NextNumber> GetNextNumber();
    Task<ScheduleEvent> GetEntityById(long ? scheduleEventId);
    Task<ScheduleEvent> GetEntityByNumber(long scheduleEventNumber);
    Task<ScheduleEventView> GetViewById(long ? scheduleEventId);
    Task<ScheduleEventView> GetViewByNumber(long scheduleEventNumber);
    Task<IQueryable<ScheduleEvent>> GetViewsByEmployeeId(long ? employeeId);
    Task<IList<ScheduleEventView>> GetViewsByCustomerId(long? customerId, long? serviceId);
    Task<IList<ScheduleEventView>> GetViewsByServiceId(long? serviceId);
}
