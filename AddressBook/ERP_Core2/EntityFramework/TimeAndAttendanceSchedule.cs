namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeAndAttendanceSchedule")]
    public partial class TimeAndAttendanceSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeAndAttendanceSchedule()
        {
            TimeAndAttendancePunchIns = new HashSet<TimeAndAttendancePunchIn>();
            TimeAndAttendanceScheduledToWorks = new HashSet<TimeAndAttendanceScheduledToWork>();
        }

        [Key]
        public long ScheduleId { get; set; }

        [StringLength(255)]
        public string ScheduleName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public long? ShiftId { get; set; }

        public long? ScheduleGroupXrefId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWorks { get; set; }
    }
}
