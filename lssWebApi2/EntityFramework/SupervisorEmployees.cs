using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class SupervisorEmployees
    {
        public long SupervisorEmployeesId { get; set; }
        public long SupervisorId { get; set; }
        public long EmployeeId { get; set; }

        public Employee Employee { get; set; }
        public Supervisor Supervisor { get; set; }
    }
}
