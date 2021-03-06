﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Supervisor
    {
        public Supervisor()
        {
            SupervisorEmployees = new HashSet<SupervisorEmployees>();
            TimeAndAttendancePunchIn = new HashSet<TimeAndAttendancePunchIn>();
        }

        public long SupervisorId { get; set; }
        public long AddressId { get; set; }
        public string SupervisorCode { get; set; }
        public long? JobTitleXrefId { get; set; }
        public long? ParentSupervisorId { get; set; }
        public bool? IsActive { get; set; }
        public string Area { get; set; }
        public string DepartmentCode { get; set; }
        public long SupervisorNumber { get; set; }

        public virtual AddressBook Address { get; set; }
        public virtual Udc JobTitleXref { get; set; }
        public virtual ICollection<SupervisorEmployees> SupervisorEmployees { get; set; }
        public virtual ICollection<TimeAndAttendancePunchIn> TimeAndAttendancePunchIn { get; set; }

    }
}