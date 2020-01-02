

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.ScheduleEventDomain
{ 

public interface IFluentScheduleEvent
    {
        IFluentScheduleEventQuery Query();
        IFluentScheduleEvent Apply();
        IFluentScheduleEvent AddScheduleEvent(ScheduleEvent scheduleEvent);
        IFluentScheduleEvent UpdateScheduleEvent(ScheduleEvent scheduleEvent);
        IFluentScheduleEvent DeleteScheduleEvent(ScheduleEvent scheduleEvent);
     	IFluentScheduleEvent UpdateScheduleEvents(List<ScheduleEvent> newObjects);
        IFluentScheduleEvent AddScheduleEvents(List<ScheduleEvent> newObjects);
        IFluentScheduleEvent DeleteScheduleEvents(List<ScheduleEvent> deleteObjects);
    }
}
