using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
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

        public virtual ICollection<AcctRec> AcctRec { get; set; }
        public virtual ICollection<Assets> Assets { get; set; }
        public virtual ICollection<Carrier> Carrier { get; set; }
        public virtual ICollection<Contract> Contract { get; set; }
        public virtual ICollection<CustomerClaim> CustomerClaimClassificationXref { get; set; }
        public virtual ICollection<CustomerClaim> CustomerClaimGroupIdXref { get; set; }
        public virtual ICollection<Employee> EmployeeEmploymentStatusXref { get; set; }
        public virtual ICollection<Employee> EmployeeJobTitleXref { get; set; }
        public virtual ICollection<LocationAddress> LocationAddress { get; set; }
        public virtual ICollection<ProjectManagementTask> ProjectManagementTask { get; set; }
        public virtual ICollection<ServiceInformation> ServiceInformation { get; set; }
        public virtual ICollection<Supervisor> Supervisor { get; set; }
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }

    }
}