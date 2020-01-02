   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.BudgetNoteDomain
{
    public  class BudgetNoteView
    {
        public long BudgetNoteId { get; set; }
        public long BudgetId { get; set; }
        public string Note { get; set; }
        public DateTime Create { get; set; }
        public long BudgetNoteNumber { get; set; }

        public virtual Budget Budget { get; set; }

    }
    public class BudgetNoteRepository: Repository<BudgetNote>, IBudgetNoteRepository
    {
        ListensoftwaredbContext _dbContext;
        public BudgetNoteRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<BudgetNote>GetEntityById(long ? budgetNoteId)
        {
			try{
            return await _dbContext.FindAsync<BudgetNote>(budgetNoteId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<BudgetNote> GetEntityByNumber(long budgetNoteNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.BudgetNote
                               where detail.BudgetNoteNumber == budgetNoteNumber
                               select detail).FirstOrDefaultAsync<BudgetNote>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<BudgetNote> FindEntityByExpression(Expression<Func<BudgetNote, bool>> predicate)
        {
            IQueryable<BudgetNote> result = _dbContext.Set<BudgetNote>().Where(predicate);

            return await result.FirstOrDefaultAsync<BudgetNote>();
        }
		  public async Task<IList<BudgetNote>> FindEntitiesByExpression(Expression<Func<BudgetNote, bool>> predicate)
        {
            IQueryable<BudgetNote> result = _dbContext.Set<BudgetNote>().Where(predicate);

            return await result.ToListAsync<BudgetNote>();
        }
		
  }
}
