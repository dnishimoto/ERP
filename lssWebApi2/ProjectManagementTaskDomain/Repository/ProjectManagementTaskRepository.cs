using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using lssWebApi2.ProjectManagementWorkOrderToEmployeeDomain;
using System.Collections.Generic;

namespace lssWebApi2.ProjectManagementTaskDomain
{
    public class ProjectManagementTaskView
    {

        public long TaskId { get; set; }
        public string Wbs { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public decimal? EstimatedHours { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? Cost { get; set; }
        public long MileStoneId { get; set; }
        public long StatusXrefId { get; set; }
        public decimal? EstimatedCost { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }
        public long ProjectId { get; set; }
        public string Account { get; set; }
        public long AccountId { get; set; }
        public long? WorkOrderId { get; set; }
        public long TaskNumber { get; set; }

        public string ProjectName { get; set; }
        public string MilestoneName { get; set; }
        public string Status { get; set; }
        public string Instructions { get; set; }
        public IList<ProjectManagementWorkOrderToEmployeeView> WorkOrderToEmployeeViews { get; set; }
    }
    public class ProjectManagementTaskRepository : Repository<ProjectManagementTask>, IProjectManagementTaskRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementTaskRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<IQueryable<ProjectManagementTask>> GetEntitiesByMilestoneId(long? milestoneId)
        {
            try
            {
                var list = from milestones in _dbContext.ProjectManagementMilestone
                                  join tasks in _dbContext.ProjectManagementTask 
                                  on milestones.MilestoneId equals tasks.MileStoneId
                                  where (milestones.MilestoneId == milestoneId)
                                  select tasks;

                await Task.Yield();

                return list.AsQueryable<ProjectManagementTask>();
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<IQueryable<ProjectManagementTask>> GetEntitiesByProjectId(long ? projectId)
        {
            try
            {
                var list = from milestones in _dbContext.ProjectManagementMilestone
                                  join tasks in _dbContext.ProjectManagementTask on milestones.MilestoneId equals tasks.MileStoneId
                                  where (milestones.ProjectId == projectId)
                                  select tasks;

                await Task.Yield();

                return list.AsQueryable<ProjectManagementTask>();
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }


        }
        public async Task<ProjectManagementTask> GetEntityById(long? taskId)
        {
            try
            {
                var query = await (from workOrder in _dbContext.ProjectManagementTask

                                   where (workOrder.TaskId == taskId)
                                   select workOrder).FirstOrDefaultAsync<ProjectManagementTask>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<ProjectManagementTask> GetEntityByNumber(long taskNumber)
        {
            try
            {
                var query = await (from workOrder in _dbContext.ProjectManagementTask

                                   where (workOrder.TaskNumber == taskNumber)
                                   select workOrder).FirstOrDefaultAsync<ProjectManagementTask>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }

       
    }
}
