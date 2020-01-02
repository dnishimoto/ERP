   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.POQuoteDomain
{
    public class POQuoteView
    {
       
        public long PoquoteId { get; set; }
        public decimal? QuoteAmount { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public long PurchaseOrderId { get; set; }
        public string Remarks { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public long PoquoteNumber { get; set; }

        public string CustomerName { get; set; }
        public string SupplierName { get; set; }

    }
    public class POQuoteRepository: Repository<Poquote>, IPOQuoteRepository
    {
        ListensoftwaredbContext _dbContext;
        public POQuoteRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<Poquote> GetEntityById(long ? poQuoteId)
        {
			try{
            return await _dbContext.FindAsync<Poquote>(poQuoteId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<Poquote> GetEntityByNumber(long poQuoteNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.Poquote
                               where detail.PoquoteNumber == poQuoteNumber
                               select detail).FirstOrDefaultAsync<Poquote>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<Poquote> FindEntityByExpression(Expression<Func<Poquote, bool>> predicate)
        {
            IQueryable<Poquote> result = _dbContext.Set<Poquote>().Where(predicate);

            return await result.FirstOrDefaultAsync<Poquote>();
        }
		  public async Task<IList<Poquote>> FindEntitiesByExpression(Expression<Func<Poquote, bool>> predicate)
        {
            IQueryable<Poquote> result = _dbContext.Set<Poquote>().Where(predicate);

            return await result.ToListAsync<Poquote>();
        }
		
  }
}
