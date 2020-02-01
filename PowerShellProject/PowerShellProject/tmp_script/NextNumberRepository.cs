   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.NextNumberDomain
{
 public class NextNumberRepository: Repository<NextNumber>, INextNumberRepository
    {
        ListensoftwaredbContext _dbContext;
        public NextNumberRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<NextNumber> GetEntitiesByExpression(Expression<Func<NextNumber, bool>> predicate)
         {
            var result =  _dbContext.Set<NextNumber>().Where(predicate);

            return result;
        }
 
  public async Task<NextNumber>GetEntityById(long ? nextNumberId)
        {
			try{
            return await _dbContext.FindAsync<NextNumber>(nextNumberId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<NextNumber> GetEntityByNumber(long nextNumberNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.NextNumber
                               where detail.NextNumberNumber == nextNumberNumber
                               select detail).FirstOrDefaultAsync<NextNumber>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<NextNumber> FindEntityByExpression(Expression<Func<NextNumber, bool>> predicate)
        {
            IQueryable<NextNumber> result = _dbContext.Set<NextNumber>().Where(predicate);

            return await result.FirstOrDefaultAsync<NextNumber>();
        }
		  public async Task<IList<NextNumber>> FindEntitiesByExpression(Expression<Func<NextNumber, bool>> predicate)
        {
            IQueryable<NextNumber> result = _dbContext.Set<NextNumber>().Where(predicate);

            return await result.ToListAsync<NextNumber>();
        }
		
  }
}
