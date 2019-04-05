using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.EntityFramework
{
    public partial class ProjectManagementWorkOrderToEmployee
    {
        public long WorkOrderToEmployeeId { get; set; }
        public long EmployeeId { get; set; }
        public long WorkOrderId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ProjectManagementWorkOrder WorkOrder { get; set; }

    }
}
