using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class ProjectManagementTaskToEmployee
    {
        public long TaskToEmployeeId { get; set; }
        public long? EmployeeId { get; set; }
        public long? TaskId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ProjectManagementTask Task { get; set; }

    }
}