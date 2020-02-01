   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.JobCostLedgerDomain
{
    public  class JobCostLedgerView
    {
        public long JobCostLedgerId { get; set; }
        public long JobMasterId { get; set; }
        public long ContractId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public long? JobPhaseId { get; set; }
        public decimal? ActualHours { get; set; }
        public decimal? ActualCost { get; set; }
        public decimal? ProjectedHours { get; set; }
        public decimal? ProjectedAmount { get; set; }
        public decimal? CommittedHours { get; set; }
        public decimal? CommittedAmount { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public string Source { get; set; }
        public long JobCostLedgerNumber { get; set; }
        public long? CustomerId { get; set; }
        public long JobCostTypeId { get; set; }
        public long? SupplierId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? InvoiceId { get; set; }
        public decimal? TaxAmount { get; set; }
    }
    public class JobCostLedgerRepository: Repository<JobCostLedger>, IJobCostLedgerRepository
    {
        ListensoftwaredbContext _dbContext;
        public JobCostLedgerRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<JobCostLedger> GetEntitiesByExpression(Expression<Func<JobCostLedger, bool>> predicate)
         {
            var result =  _dbContext.Set<JobCostLedger>().Where(predicate);

            return result;
        }
 
  public async Task<JobCostLedger>GetEntityById(long ? jobCostLedgerId)
        {
			try{
            return await _dbContext.FindAsync<JobCostLedger>(jobCostLedgerId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<JobCostLedger> GetEntityByNumber(long jobCostLedgerNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.JobCostLedger
                               where detail.JobCostLedgerNumber == jobCostLedgerNumber
                               select detail).FirstOrDefaultAsync<JobCostLedger>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<JobCostLedger> FindEntityByExpression(Expression<Func<JobCostLedger, bool>> predicate)
        {
            IQueryable<JobCostLedger> result = _dbContext.Set<JobCostLedger>().Where(predicate);

            return await result.FirstOrDefaultAsync<JobCostLedger>();
        }
		  public async Task<IList<JobCostLedger>> FindEntitiesByExpression(Expression<Func<JobCostLedger, bool>> predicate)
        {
            IQueryable<JobCostLedger> result = _dbContext.Set<JobCostLedger>().Where(predicate);

            return await result.ToListAsync<JobCostLedger>();
        }
		
  }
}
