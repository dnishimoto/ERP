namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScheduleEvent")]
    public partial class ScheduleEvent
    {
        public long ScheduleEventId { get; set; }

        public long EmployeeId { get; set; }

        public DateTime? EventDateTime { get; set; }

        public long ServiceId { get; set; }

        public long? DurationMinutes { get; set; }

        public long? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ServiceInformation ServiceInformation { get; set; }
    }
}
