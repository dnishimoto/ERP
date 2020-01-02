using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{
    public interface IProjectManagementWorkOrderRepository
    {
        Task<ProjectManagementWorkOrder> GetEntityById(long ? workOrderId);
        Task<ProjectManagementWorkOrder> GetEntityByNumber(long workOrderNumber);
        Task<IQueryable<ProjectManagementWorkOrder>> GetEntitiesByProjectId(long? projectId);
    }
}
