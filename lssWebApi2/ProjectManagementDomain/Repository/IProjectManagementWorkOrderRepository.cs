using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain.Repository
{
    public interface IProjectManagementWorkOrderRepository
    {
        Task<ProjectManagementWorkOrderView> GetWorkOrderViewById(long workOrderId);
        Task<ProjectManagementWorkOrder> GetWorkOrderById(long workOrderId);
        Task<ProjectManagementWorkOrder> GetWorkOrderByNumber(long workOrderNumber);
    }
}
