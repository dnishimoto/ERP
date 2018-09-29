using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ProjectManagementTaskToEmployee
    {
        public long TaskToEmployeeId { get; set; }
        public long? EmployeeId { get; set; }
        public long? TaskId { get; set; }

        public Employee Employee { get; set; }
        public ProjectManagementTask Task { get; set; }
    }
}
