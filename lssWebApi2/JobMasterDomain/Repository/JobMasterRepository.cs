   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.JobMasterDomain
{
    public  class JobMasterView
    {
        public long JobMasterId { get; set; }
        public long ContractId { get; set; }
        public long CustomerId { get; set; }
        public string JobDescription { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public long? ProjectManagerId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public decimal? TotalCommittedAmount { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? EstimatedHours { get; set; }
        public decimal? ProjectedAmount { get; set; }
        public decimal? ProjectHours { get; set; }
        public decimal? RemainingCommittedAmount { get; set; }
        public decimal? RetainageAmount { get; set; }
        public long JobMasterNumber { get; set; }

        public string CustomerName { get; set; }
        public string ContractTitle { get; set; }
    }
    public class JobMasterRepository: Repository<JobMaster>, IJobMasterRepository
    {
        ListensoftwaredbContext _dbContext;
        public JobMasterRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<JobMaster> GetEntitiesByExpression(Expression<Func<JobMaster, bool>> predicate)
         {
            var result =  _dbContext.Set<JobMaster>().Where(predicate);

            return result;
        }
 
  public async Task<JobMaster>GetEntityById(long ? jobMasterId)
        {
			try{
            return await _dbContext.FindAsync<JobMaster>(jobMasterId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<JobMaster> GetEntityByNumber(long jobMasterNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.JobMaster
                               where detail.JobMasterNumber == jobMasterNumber
                               select detail).FirstOrDefaultAsync<JobMaster>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<JobMaster> FindEntityByExpression(Expression<Func<JobMaster, bool>> predicate)
        {
            IQueryable<JobMaster> result = _dbContext.Set<JobMaster>().Where(predicate);

            return await result.FirstOrDefaultAsync<JobMaster>();
        }
		  public async Task<IList<JobMaster>> FindEntitiesByExpression(Expression<Func<JobMaster, bool>> predicate)
        {
            IQueryable<JobMaster> result = _dbContext.Set<JobMaster>().Where(predicate);

            return await result.ToListAsync<JobMaster>();
        }
		
  }
}
