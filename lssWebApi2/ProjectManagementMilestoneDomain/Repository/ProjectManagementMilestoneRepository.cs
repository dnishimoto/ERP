using System;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.ProjectManagementDomain;
using Microsoft.EntityFrameworkCore;

namespace lssWebApi2.ProjectManagementMilestoneDomain
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

        public string ProjectName { get; set; }
    }
    public class RollupTaskToMilestoneView
    {
        public long MilestoneId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public decimal? Cost { get; set; }
    }
    public class ProjectManagementMilestoneRepository : Repository<ProjectManagementMilestone>, IProjectManagementMilestoneRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementMilestoneRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<RollupTaskToMilestoneView> GetTaskToMilestoneRollupViewById(long? milestoneId)
        {

            RollupTaskToMilestoneView view = await (from e in _dbContext.ProjectManagementMilestone
                                                    join f in _dbContext.ProjectManagementTask
                                                    on e.MilestoneId equals f.MileStoneId
                                                    where e.MilestoneId == milestoneId
                                                    group f by f.MileStoneId into pg

                                                    select new RollupTaskToMilestoneView
                                                    {
                                                        MilestoneId = milestoneId ?? 0,
                                                        EstimatedHours = pg.Sum(e => e.EstimatedHours),
                                                        ActualDays =pg.Sum(e=>e.ActualDays),
                                                        EstimatedDays =pg.Sum(e=>e.EstimatedDays),
                                                        ActualHours =pg.Sum(e=>e.ActualHours),
                                                        ActualStartDate =pg.Min(e=>e.ActualStartDate),
                                                        ActualEndDate =pg.Max(e=>e.ActualEndDate),
                                                        EstimatedStartDate =pg.Min(e=>e.EstimatedStartDate),
                                                        EstimatedEndDate =pg.Max(e=>e.EstimatedEndDate),
                                                        Cost=pg.Sum(e=>e.Cost)
                                                    }).FirstOrDefaultAsync<RollupTaskToMilestoneView>();
            return view;





        }
        public async Task<ProjectManagementMilestone> GetEntityById(long? mileStoneId)
        {
            try
            {
                var query = await (from milestone in _dbContext.ProjectManagementMilestone

                                   where (milestone.MilestoneId == mileStoneId)
                                   select milestone).FirstOrDefaultAsync<ProjectManagementMilestone>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
        public async Task<IQueryable<ProjectManagementMilestone>> GetEntitiesByProjectId(long ? projectId)
        {
            try
            {
                var query = (from milestone in _dbContext.ProjectManagementMilestone

                                where (milestone.ProjectId == projectId)
                                select milestone);

                await Task.Yield();

                return query;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }

        }
        public async Task<ProjectManagementMilestone> GetEntityByNumber(long mileStoneNumber)
        {
            try
            {
                var query = await (from milestone in _dbContext.ProjectManagementMilestone

                                   where (milestone.MileStoneNumber == mileStoneNumber)
                                   select milestone).FirstOrDefaultAsync<ProjectManagementMilestone>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
      
    }
}
