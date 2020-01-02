using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementWorkOrderDomain
{
    public class ProjectManagementWorkOrderView
    {
        public long WorkOrderId { get; set; }
        public long? WorkOrderNumber { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? EstimatedHours { get; set; }
        public string AccountNumber { get; set; }
        public string Instructions { get; set; }
        public long ProjectId { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public long AccountId { get; set; }

        public string ProjectName { get; set; }
        public string AccountDescription { get; set; }
        public string SupCode { get; set; }
        public string ThirdAccount { get; set; }

    }
    public class ProjectManagementWorkOrderRepository : Repository<ProjectManagementWorkOrder>, IProjectManagementWorkOrderRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementWorkOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IQueryable<ProjectManagementWorkOrder>> GetEntitiesByProjectId(long? projectId)
        {

            try
            {

                IQueryable<ProjectManagementWorkOrder> list = (from workorder in _dbContext.ProjectManagementWorkOrder

                                  where workorder.ProjectId == projectId
                                  select workorder).AsQueryable<ProjectManagementWorkOrder>();
                await Task.Yield();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

        public async Task<ProjectManagementWorkOrder> GetEntityById(long ? workOrderId)
        {
            try
            {
                var query = await (from workOrder in _dbContext.ProjectManagementWorkOrder

                                   where (workOrder.WorkOrderId == workOrderId)
                                   select workOrder).FirstOrDefaultAsync<ProjectManagementWorkOrder>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<ProjectManagementWorkOrder> GetEntityByNumber(long workOrderNumber)
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
