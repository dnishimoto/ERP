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
        public long Id { get; set; }

        public long? EmployeeAddressId { get; set; }

        public DateTime? EventDateTime { get; set; }

        public long? ServiceId { get; set; }

        public long? DurationMinutes { get; set; }

        public long? CustomerAddressId { get; set; }

        public virtual AddressBook CustomerAddressBook { get; set; }

        public virtual AddressBook EmployeeAddressBook { get; set; }
       
        public virtual ServiceInformation ServiceInformation { get; set; }
    }
}
