using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain.Repository
{
    public class ProjectManagementWorkOrderRepository : Repository<ProjectManagementWorkOrder>
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementWorkOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<ProjectManagementWorkOrder> GetWorkOrderByNumber(long workOrderNumber)
        {
            try
            {
                var query = await (from workOrder in _dbContext.ProjectManagementWorkOrder

                                   where (workOrder.WorkOrderNumber == workOrderNumber)
                                   select workOrder).FirstOrDefaultAsync<ProjectManagementWorkOrder>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
    }
}
