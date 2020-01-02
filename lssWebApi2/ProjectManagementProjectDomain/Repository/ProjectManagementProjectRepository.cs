using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.ProjectManagementDomain;
using Microsoft.EntityFrameworkCore;

namespace lssWebApi2.ProjectManagementDomain
{
    public class ProjectManagementProjectView
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public decimal? EstimatedHours { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public decimal? Cost { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? BudgetHours { get; set; }
        public long? ProjectNumber { get; set; }
    }
    public class RollupTaskToProjectView
    {
        public long ProjectId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public int? ActualDays { get; set; }
        public int? EstimatedDays { get; set; }
        public decimal? ActualHours { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public decimal? Cost { get; set; }
        public int ItemCount { get; set; }
    }
    public class ProjectManagementProjectRepository: Repository<ProjectManagementProject> , IProjectManagementProjectRepository
    {
        ListensoftwaredbContext _dbContext;
        public ProjectManagementProjectRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<RollupTaskToProjectView> GetTaskToProjectRollupViewById(long? projectId)
        {

            RollupTaskToProjectView view = await (from e in _dbContext.ProjectManagementProject
                                                    join f in _dbContext.ProjectManagementTask
                                                    on e.ProjectId equals f.ProjectId
                                                    where e.ProjectId == projectId
                                                    group f by f.ProjectId into pg

                                                    select new RollupTaskToProjectView
                                                    {
                                                        ProjectId = projectId ?? 0,
                                                        EstimatedHours = pg.Sum(e => e.EstimatedHours),
                                                        ActualDays = pg.Sum(e => e.ActualDays),
                                                        EstimatedDays = pg.Sum(e => e.EstimatedDays),
                                                        ActualHours = pg.Sum(e => e.ActualHours),
                                                        ActualStartDate = pg.Min(e => e.ActualStartDate),
                                                        ActualEndDate = pg.Max(e => e.ActualEndDate),
                                                        EstimatedStartDate = pg.Min(e => e.EstimatedStartDate),
                                                        EstimatedEndDate = pg.Max(e => e.EstimatedEndDate),
                                                        Cost = pg.Sum(e => e.Cost),
                                                        ItemCount=pg.Count()
                                                    }).FirstOrDefaultAsync<RollupTaskToProjectView>();
            return view;





        }
        public async Task<ProjectManagementProject> GetEntityById(long ? projectId)
        {
            try
            {
                var query = await (from project in _dbContext.ProjectManagementProject

                                   where (project.ProjectId == projectId)
                                   select project).FirstOrDefaultAsync<ProjectManagementProject>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
      
        public async Task<ProjectManagementProject> GetEntityByNumber(long projectNumber)
        {
            try
            {
                var query = await(from project in _dbContext.ProjectManagementProject

                                 where (project.ProjectNumber==projectNumber)
                                 select project).FirstOrDefaultAsync<ProjectManagementProject>();

                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }
        }
     
      
       
      
    }
}
