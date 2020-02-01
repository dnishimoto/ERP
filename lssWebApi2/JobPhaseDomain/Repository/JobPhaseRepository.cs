   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.JobPhaseDomain
{
    public class JobPhaseView
    {
        public long JobPhaseId { get; set; }
        public int PhaseGroup { get; set; }
        public long JobMasterId { get; set; }
        public string Phase { get; set; }
        public long ContractId { get; set; }
        public long JobPhaseNumber { get; set; }
        public long JobCostTypeId { get; set; }

        public string ContractTitle{get;set;}
        public string JobDescription{ get; set; }
        public string CostCode { get; set; }

    }
    public class JobPhaseRepository: Repository<JobPhase>, IJobPhaseRepository
    {
        ListensoftwaredbContext _dbContext;
        public JobPhaseRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<JobPhase> GetEntitiesByExpression(Expression<Func<JobPhase, bool>> predicate)
         {
            var result =  _dbContext.Set<JobPhase>().Where(predicate);

            return result;
        }
        public async Task<IList<JobPhase>> GetEntitiesByJobMasterId(long? jobMasterId)
        {
            try
            {
                var query = await (from detail in _dbContext.JobPhase
                                   where detail.JobMasterId == jobMasterId
                                   select detail).ToListAsync<JobPhase>();
                return query;
            }
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
  public async Task<JobPhase>GetEntityById(long ? jobPhaseId)
        {
			try{
            return await _dbContext.FindAsync<JobPhase>(jobPhaseId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<JobPhase> GetEntityByNumber(long jobPhaseNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.JobPhase
                               where detail.JobPhaseNumber == jobPhaseNumber
                               select detail).FirstOrDefaultAsync<JobPhase>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<JobPhase> FindEntityByExpression(Expression<Func<JobPhase, bool>> predicate)
        {
            IQueryable<JobPhase> result = _dbContext.Set<JobPhase>().Where(predicate);

            return await result.FirstOrDefaultAsync<JobPhase>();
        }
		  public async Task<IList<JobPhase>> FindEntitiesByExpression(Expression<Func<JobPhase, bool>> predicate)
        {
            IQueryable<JobPhase> result = _dbContext.Set<JobPhase>().Where(predicate);

            return await result.ToListAsync<JobPhase>();
        }
		
  }
}
