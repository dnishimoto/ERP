namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            ProjectManagementTaskToEmployees = new HashSet<ProjectManagementTaskToEmployee>();
            ScheduleEvents = new HashSet<ScheduleEvent>();
            TimeAndAttendancePunchIns = new HashSet<TimeAndAttendancePunchIn>();
        }

        public long EmployeeId { get; set; }

        public long AddressId { get; set; }

        public long? PrimaryLocationId { get; set; }

        public long? PrimaryEmailId { get; set; }

        public long? PrimaryPhoneId { get; set; }

        public long? MailingLocationId { get; set; }

        [StringLength(50)]
        public string TaxIdentification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleEvent> ScheduleEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIns { get; set; }
    }
}
