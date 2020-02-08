   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.AccountReceivableDetailDomain
{
 public class AccountReceivableDetailRepository: Repository<AccountReceivableDetail>, IAccountReceivableDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public AccountReceivableDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<AccountReceivableDetail> GetEntitiesByExpression(Expression<Func<AccountReceivableDetail, bool>> predicate)
         {
            var result =  _dbContext.Set<AccountReceivableDetail>().Where(predicate);

            return result;
        }
 
  public async Task<AccountReceivableDetail>GetEntityById(long ? accountReceivableDetailId)
        {
			try{
            return await _dbContext.FindAsync<AccountReceivableDetail>(accountReceivableDetailId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<AccountReceivableDetail> GetEntityByNumber(long accountReceivableDetailNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.AccountReceivableDetail
                               where detail.AccountReceivableDetailNumber == accountReceivableDetailNumber
                               select detail).FirstOrDefaultAsync<AccountReceivableDetail>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<AccountReceivableDetail> FindEntityByExpression(Expression<Func<AccountReceivableDetail, bool>> predicate)
        {
            IQueryable<AccountReceivableDetail> result = _dbContext.Set<AccountReceivableDetail>().Where(predicate);

            return await result.FirstOrDefaultAsync<AccountReceivableDetail>();
        }
		  public async Task<IList<AccountReceivableDetail>> FindEntitiesByExpression(Expression<Func<AccountReceivableDetail, bool>> predicate)
        {
            IQueryable<AccountReceivableDetail> result = _dbContext.Set<AccountReceivableDetail>().Where(predicate);

            return await result.ToListAsync<AccountReceivableDetail>();
        }
		
  }
}
