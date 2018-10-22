using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Employee
    {
        public Employee()
        {
            CustomerClaim = new HashSet<CustomerClaim>();
            ProjectManagementTaskToEmployee = new HashSet<ProjectManagementTaskToEmployee>();
            ScheduleEvent = new HashSet<ScheduleEvent>();
            SupervisorEmployees = new HashSet<SupervisorEmployees>();
            TimeAndAttendancePunchIn = new HashSet<TimeAndAttendancePunchIn>();
            TimeAndAttendanceScheduledToWork = new HashSet<TimeAndAttendanceScheduledToWork>();
        }

        public long EmployeeId { get; set; }
        public long AddressId { get; set; }
        public long JobTitleXrefId { get; set; }
        public long EmploymentStatusXrefId { get; set; }
        public DateTime? HiredDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string TaxIdentification { get; set; }

        public virtual AddressBook Address { get; set; }
        public virtual Udc EmploymentStatusXref { get; set; }
        public virtual Udc JobTitleXref { get; set; }
        public virtual ICollection<CustomerClaim> CustomerClaim { get; set; }
        public virtual ICollection<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployee { get; set; }
        public virtual ICollection<ScheduleEvent> ScheduleEvent { get; set; }
        public virtual ICollection<SupervisorEmployees> SupervisorEmployees { get; set; }
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
        public virtual ICollection<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWork { get; set; }

    }
}
