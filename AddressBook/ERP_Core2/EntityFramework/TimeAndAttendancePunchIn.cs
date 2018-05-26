namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeAndAttendancePunchIn")]
    public partial class TimeAndAttendancePunchIn
    {
        [Key]
        public long TimePunchinId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PunchinDate { get; set; }

        [StringLength(14)]
        public string PunchinDateTime { get; set; }

        [StringLength(14)]
        public string PunchoutDateTime { get; set; }

        public long? JobCodeXrefId { get; set; }

        public bool? Approved { get; set; }

        public long? EmployeeId { get; set; }

        public long? SupervisorId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProcessedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PunchoutDate { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public long? ShiftId { get; set; }

        [StringLength(14)]
        public string mealPunchin { get; set; }

        [StringLength(14)]
        public string mealPunchout { get; set; }

        public bool? ScheduledToWork { get; set; }

        public long? TypeOfTimeUdcXrefId { get; set; }

        public long? ApprovingAddressId { get; set; }

        public long? PayCodeXrefId { get; set; }

        public long? ScheduleId { get; set; }

        public int? DurationInMinutes { get; set; }

        public virtual Supervisor Supervisor { get; set; }

        public virtual TimeAndAttendanceSchedule TimeAndAttendanceSchedule { get; set; }

        public virtual TimeAndAttendanceShift TimeAndAttendanceShift { get; set; }
    }
}
