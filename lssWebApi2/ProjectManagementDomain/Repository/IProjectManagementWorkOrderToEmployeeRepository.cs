using ERP_Core2.AddressBookDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain.Repository
{
    public interface IProjectManagementWorkOrderToEmployeeRepository
    {
        Task<IEnumerable<EmployeeView>> GetEmployeeByWorkOrderId(long workOrderId);
    }
}
