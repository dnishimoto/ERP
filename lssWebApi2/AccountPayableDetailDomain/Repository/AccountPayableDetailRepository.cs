   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.AccountPayableDetailDomain
{

        public  class AccountPayableDetailView
    {
        public long AccountPayableDetailId { get; set; }
        public long InvoiceId { get; set; }
        public long InvoiceDetailId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public int? QuantityReceived { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountPaid { get; set; }
        public long? PurchaseOrderDetailId { get; set; }
        public long? SalesOrderDetailId { get; set; }
        public long? ItemId { get; set; }
        public string ExtendedDescription { get; set; }
        public long AccountPayableDetailNumber { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? CustomerId { get; set; }
        public long? SupplierId { get; set; }
        public long? AccountPayableId { get; set; }

    

    }
    public class AccountPayableDetailRepository: Repository<AccountPayableDetail>, IAccountPayableDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public AccountPayableDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
  
		 public IQueryable<AccountPayableDetail> GetEntitiesByExpression(Expression<Func<AccountPayableDetail, bool>> predicate)
         {
            var result =  _dbContext.Set<AccountPayableDetail>().Where(predicate);

            return result;
        }
 
  public async Task<AccountPayableDetail>GetEntityById(long ? accountPyableDetailId)
        {
			try{
            return await _dbContext.FindAsync<AccountPayableDetail>(accountPyableDetailId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<AccountPayableDetail> GetEntityByNumber(long accountPyableDetailNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.AccountPayableDetail
                               where detail.AccountPayableDetailNumber == accountPyableDetailNumber
                               select detail).FirstOrDefaultAsync<AccountPayableDetail>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<AccountPayableDetail> FindEntityByExpression(Expression<Func<AccountPayableDetail, bool>> predicate)
        {
            IQueryable<AccountPayableDetail> result = _dbContext.Set<AccountPayableDetail>().Where(predicate);

            return await result.FirstOrDefaultAsync<AccountPayableDetail>();
        }
		  public async Task<IList<AccountPayableDetail>> FindEntitiesByExpression(Expression<Func<AccountPayableDetail, bool>> predicate)
        {
            IQueryable<AccountPayableDetail> result = _dbContext.Set<AccountPayableDetail>().Where(predicate);

            return await result.ToListAsync<AccountPayableDetail>();
        }
		
  }
}
