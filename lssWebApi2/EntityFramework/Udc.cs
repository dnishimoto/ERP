using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Udc
    {
        public Udc()
        {
            AcctRec = new HashSet<AcctRec>();
            Assets = new HashSet<Assets>();
            Carrier = new HashSet<Carrier>();
            Contract = new HashSet<Contract>();
            CustomerClaimClassificationXref = new HashSet<CustomerClaim>();
            CustomerClaimGroupIdXref = new HashSet<CustomerClaim>();
            EmployeeEmploymentStatusXref = new HashSet<Employee>();
            EmployeeJobTitleXref = new HashSet<Employee>();
            LocationAddress = new HashSet<LocationAddress>();
            ProjectManagementTask = new HashSet<ProjectManagementTask>();
            ServiceInformation = new HashSet<ServiceInformation>();
            Supervisor = new HashSet<Supervisor>();
            TimeAndAttendancePunchIn = new HashSet<TimeAndAttendancePunchIn>();
        }

        public long XrefId { get; set; }
        public string ProductCode { get; set; }
        public string KeyCode { get; set; }
        public string Value { get; set; }

        public ICollection<AcctRec> AcctRec { get; set; }
        public ICollection<Assets> Assets { get; set; }
        public ICollection<Carrier> Carrier { get; set; }
        public ICollection<Contract> Contract { get; set; }
        public ICollection<CustomerClaim> CustomerClaimClassificationXref { get; set; }
        public ICollection<CustomerClaim> CustomerClaimGroupIdXref { get; set; }
        public ICollection<Employee> EmployeeEmploymentStatusXref { get; set; }
        public ICollection<Employee> EmployeeJobTitleXref { get; set; }
        public ICollection<LocationAddress> LocationAddress { get; set; }
        public ICollection<ProjectManagementTask> ProjectManagementTask { get; set; }
        public ICollection<ServiceInformation> ServiceInformation { get; set; }
        public ICollection<Supervisor> Supervisor { get; set; }
        public ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }
    }
}
