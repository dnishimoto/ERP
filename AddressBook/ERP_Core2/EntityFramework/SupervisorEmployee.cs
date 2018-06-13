namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SupervisorEmployee
    {
        [Key]
        public long SupervisorEmployeesId { get; set; }

        public long SupervisorId { get; set; }

        public long EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Supervisor Supervisor { get; set; }
    }
}
