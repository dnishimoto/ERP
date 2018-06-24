namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supervisor")]
    public partial class Supervisor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supervisor()
        {
            SupervisorEmployees = new HashSet<SupervisorEmployee>();
            TimeAndAttendancePunchIns = new HashSet<TimeAndAttendancePunchIn>();
        }

        public long SupervisorId { get; set; }

        public long AddressId { get; set; }

        [StringLength(20)]
        public string SupervisorCode { get; set; }

        public long? JobTitleXrefId { get; set; }

        public long? ParentSupervisorId { get; set; }

        public bool? IsActive { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual UDC UDC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupervisorEmployee> SupervisorEmployees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIns { get; set; }
    }
}
