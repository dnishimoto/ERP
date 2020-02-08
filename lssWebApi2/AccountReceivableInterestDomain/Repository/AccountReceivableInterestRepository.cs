   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.AccountReceivableInterestDomain
{
    public class AccountReceivableInterestView
    {
        public long AcctRecInterestId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? InterestRate { get; set; }
        public DateTime? InterestFromDate { get; set; }
        public DateTime? InterestToDate { get; set; }
        public long DocNumber { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public long CustomerId { get; set; }
        public string AcctRecDocType { get; set; }
        public DateTime? LastInterestDueDate { get; set; }
        public long AccountReceivableId { get; set; }
        public long AccountReceivableInterestNumber { get; set; }

        public string CustomerName { get; set; }


    }
    public class AccountReceivableInterestRepository: Repository<AccountReceivableInterest>, IAccountReceivableInterestRepository
    {
        ListensoftwaredbContext _dbContext;
        public AccountReceivableInterestRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<AccountReceivableInterest>GetEntityById(long ? accountReceivableInterestId)
        {
			try{
            return await _dbContext.FindAsync<AccountReceivableInterest>(accountReceivableInterestId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<AccountReceivableInterest> GetEntityByNumber(long accountReceivableInterestNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.AccountReceivableInterest
                               where detail.AccountReceivableInterestNumber == accountReceivableInterestNumber
                               select detail).FirstOrDefaultAsync<AccountReceivableInterest>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
	
		public async Task<AccountReceivableInterest> FindEntityByExpression(Expression<Func<AccountReceivableInterest, bool>> predicate)
        {
            IQueryable<AccountReceivableInterest> result = _dbContext.Set<AccountReceivableInterest>().Where(predicate);

            return await result.FirstOrDefaultAsync<AccountReceivableInterest>();
        }
		  public async Task<IList<AccountReceivableInterest>> FindEntitiesByExpression(Expression<Func<AccountReceivableInterest, bool>> predicate)
        {
            IQueryable<AccountReceivableInterest> result = _dbContext.Set<AccountReceivableInterest>().Where(predicate);

            return await result.ToListAsync<AccountReceivableInterest>();
        }
		
  }
}
