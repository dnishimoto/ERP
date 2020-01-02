   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.AccountReceivableDomain
{
    public class AccountReceivableFeeView
    {
        public long AcctRecFeeId { get; set; }
        public decimal? FeeAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public long CustomerId { get; set; }
        public long DocNumber { get; set; }
        public string AcctRecDocType { get; set; }
        public long AcctRecId { get; set; }
        public long AccountReceivableFeeNumber { get; set; }

        public string CustomerName { get; set; }

    }
    public class AccountReceivableFeeRepository: Repository<AccountReceivableFee>, IAccountReceivableFeeRepository
    {
        ListensoftwaredbContext _dbContext;
        public AccountReceivableFeeRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<AccountReceivableFee>GetEntityById(long ? accountReceivableFeeId)
        {
			try{
            return await _dbContext.FindAsync<AccountReceivableFee>(accountReceivableFeeId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<Decimal> GetFeeAmountById(long? acctRecId)
        {

            var fee_query = await (from e in _dbContext.AccountReceivableFee
                                   where e.AcctRecId == acctRecId
                                   group e by e.AcctRecId
                                            into pg
                                   select new { FeeAmount = pg.Sum(e => e.FeeAmount ?? 0) }
                                        ).FirstOrDefaultAsync();
            return fee_query.FeeAmount;
        }
        public async Task<AccountReceivableFee> GetEntityByNumber(long accountReceivableFeeNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.AccountReceivableFee
                               where detail.AccountReceivableFeeNumber == accountReceivableFeeNumber
                               select detail).FirstOrDefaultAsync<AccountReceivableFee>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<AccountReceivableFee> FindEntityByExpression(Expression<Func<AccountReceivableFee, bool>> predicate)
        {
            IQueryable<AccountReceivableFee> result = _dbContext.Set<AccountReceivableFee>().Where(predicate);

            return await result.FirstOrDefaultAsync<AccountReceivableFee>();
        }
		  public async Task<IList<AccountReceivableFee>> FindEntitiesByExpression(Expression<Func<AccountReceivableFee, bool>> predicate)
        {
            IQueryable<AccountReceivableFee> result = _dbContext.Set<AccountReceivableFee>().Where(predicate);

            return await result.ToListAsync<AccountReceivableFee>();
        }
		
  }
}
