using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ProjectManagementDomain
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
    }
    public class ProjectManagementWorkOrderRepository : Repository<ProjectManagementWorkOrder>, IProjectManagementWorkOrderRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementWorkOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<ProjectManagementWorkOrderView> GetWorkOrderViewById(long workOrderId)
        {
            try
            {
                var query = await (from workOrder in _dbContext.ProjectManagementWorkOrder

                                   where (workOrder.WorkOrderId == workOrderId)
                                   select workOrder).FirstOrDefaultAsync<ProjectManagementWorkOrder>();

                Mapper mapper = new Mapper();

                ProjectManagementWorkOrderView view = mapper.Map<ProjectManagementWorkOrderView>(query);
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<ProjectManagementWorkOrder> GetWorkOrderById(long workOrderId)
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
