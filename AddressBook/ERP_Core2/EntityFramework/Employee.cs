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
            SupervisorEmployees = new HashSet<SupervisorEmployee>();
            TimeAndAttendancePunchIns = new HashSet<TimeAndAttendancePunchIn>();
        }

        public long EmployeeId { get; set; }

        public long AddressId { get; set; }

        public long JobTitleXrefId { get; set; }

        public long EmploymentStatusXRefId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HiredDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TerminationDate { get; set; }

        [StringLength(50)]
        public string TaxIdentification { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual UDC UDC { get; set; }

        public virtual UDC UDC1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleEvent> ScheduleEvents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupervisorEmployee> SupervisorEmployees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIns { get; set; }
    }
}
