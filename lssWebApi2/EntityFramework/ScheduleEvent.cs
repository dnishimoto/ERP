using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ScheduleEvent
    {
        public long ScheduleEventId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime? EventDateTime { get; set; }
        public long ServiceId { get; set; }
        public long? DurationMinutes { get; set; }
        public long? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public ServiceInformation Service { get; set; }
    }
}
