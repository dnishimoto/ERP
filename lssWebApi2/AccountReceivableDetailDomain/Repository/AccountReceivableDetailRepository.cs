   

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
    public class AccountReceivableDetailView
    {
        public long AccountReceivableDetailId { get; set; }
        public long InvoiceId { get; set; }
        public long InvoiceDetailId { get; set; }
        public long AccountReceivableId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountReceived { get; set; }
        public long? PurchaseOrderDetailId { get; set; }
        public long? SalesOrderDetailId { get; set; }
        public long? ItemId { get; set; }
        public long AccountReceivableDetailNumber { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? CustomerId { get; set; }
        public long? SupplierId { get; set; }
        public long? QuantityDelivered { get; set; }
        public string Comment { get; set; }
        public string TypeOfPayment { get; set; }
    }
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
