namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeAndAttendanceShift")]
    public partial class TimeAndAttendanceShift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeAndAttendanceShift()
        {
            TimeAndAttendancePunchIns = new HashSet<TimeAndAttendancePunchIn>();
            TimeAndAttendanceSchedules = new HashSet<TimeAndAttendanceSchedule>();
        }

        [Key]
        public long ShiftId { get; set; }

        [StringLength(20)]
        public string ShiftName { get; set; }

        public int? ShiftStartTime { get; set; }

        public int? ShiftEndTime { get; set; }

        [StringLength(50)]
        public string ShiftType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TimeAndAttendanceSchedule> TimeAndAttendanceSchedules { get; set; }
    }
}
