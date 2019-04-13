using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.ProjectManagementDomain
{
    public class ProjectManagementMilestoneView
    {
        public long MilestoneId { get; set; }
        public long MileStoneNumber { get; set; }
        public string MilestoneName { get; set; }
        public long? ProjectId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public decimal? Cost { get; set; }
        public string Wbs { get; set; }
    }
    public class ProjectManagementMilestoneRepository : Repository<ProjectManagementMilestones>
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementMilestoneRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IQueryable<ProjectManagementMilestones>> GetTasksByMilestoneId(long milestoneId)
        {
            try
            {
                var list = await base.GetObjectsQueryable(e => e.MilestoneId == milestoneId, nameof(ProjectManagementTask)).ToListAsync();

                return list.AsQueryable<ProjectManagementMilestones>();
          
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }

        }
    }
}
