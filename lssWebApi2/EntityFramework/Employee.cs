using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
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

        public AddressBook Address { get; set; }
        public Udc EmploymentStatusXref { get; set; }
        public Udc JobTitleXref { get; set; }
        public ICollection<CustomerClaim> CustomerClaim { get; set; }
        public ICollection<ProjectManagementTaskToEmployee> ProjectManagementTaskToEmployee { get; set; }
        public ICollection<ScheduleEvent> ScheduleEvent { get; set; }
        public ICollection<SupervisorEmployees> SupervisorEmployees { get; set; }
        public ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
        public ICollection<TimeAndAttendanceScheduledToWork> TimeAndAttendanceScheduledToWork { get; set; }
    }
}
