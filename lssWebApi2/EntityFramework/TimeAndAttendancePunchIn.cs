using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class TimeAndAttendancePunchIn
    {
        public long TimePunchinId { get; set; }
        public DateTime? PunchinDate { get; set; }
        public string PunchinDateTime { get; set; }
        public string PunchoutDateTime { get; set; }
        public long JobCodeXrefId { get; set; }
        public bool? Approved { get; set; }
        public long EmployeeId { get; set; }
        public long SupervisorId { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime? PunchoutDate { get; set; }
        public string Note { get; set; }
        public long? ShiftId { get; set; }
        public string MealPunchin { get; set; }
        public string MealPunchout { get; set; }
        public bool? ScheduledToWork { get; set; }
        public long TypeOfTimeUdcXrefId { get; set; }
        public long ApprovingAddressId { get; set; }
        public long PayCodeXrefId { get; set; }
        public long? ScheduleId { get; set; }
        public int? DurationInMinutes { get; set; }
        public int? MealDurationInMinutes { get; set; }
        public string TypeOfTime { get; set; }
        public string PayCode { get; set; }
        public string JobCode { get; set; }
        public string TransferJobCode { get; set; }
        public long? TransferSupervisorId { get; set; }
        public long? TaskStatusXrefId { get; set; }
        public string TaskStatus { get; set; }
        public string Account { get; set; }
        public string AreaCode { get; set; }
        public string DepartmentCode { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual TimeAndAttendanceShift Shift { get; set; }
        public virtual TimeAndAttendanceSchedule Supervisor { get; set; }
        public virtual Supervisor SupervisorNavigation { get; set; }
        public virtual Udc TypeOfTimeUdcXref { get; set; }

    }
}
