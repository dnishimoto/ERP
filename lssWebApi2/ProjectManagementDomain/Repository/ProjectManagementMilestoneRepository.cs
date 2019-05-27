using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.ProjectManagementDomain.Repository;
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
    public class ProjectManagementMilestoneRepository : Repository<ProjectManagementMilestones>, IProjectManagementMilestoneRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementMilestoneRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<ProjectManagementMilestones> GetMileStoneById(long mileStoneId)
        {
            try
            {
                var query = await (from milestone in _dbContext.ProjectManagementMilestones

                                   where (milestone.MilestoneId == mileStoneId)
                                   select milestone).FirstOrDefaultAsync<ProjectManagementMilestones>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<ProjectManagementMilestones> GetMileStoneByNumber(long mileStoneNumber)
        {
            try
            {
                var query = await (from milestone in _dbContext.ProjectManagementMilestones

                                   where (milestone.MileStoneNumber == mileStoneNumber)
                                   select milestone).FirstOrDefaultAsync<ProjectManagementMilestones>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
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
