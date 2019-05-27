using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.ProjectManagementDomain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ProjectManagementDomain
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
        public string AccountNumber { get; set; }
        public long? WorkOrderId { get; set; }

        public string ProjectName { get; set; }
    }
    public class ProjectManagementTaskRepository : Repository<ProjectManagementTask>,IProjectManagementTaskRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementTaskRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IQueryable<ProjectManagementTask>> GetEmployeeByTaskId(int taskId)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.TaskId == taskId, "").ToListAsync();

                return list.AsQueryable<ProjectManagementTask>();
            }
            catch(Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
