   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.InvoiceDetailDomain
{
    public class InvoiceDetailView
    {

        public long InvoiceDetailId { get; set; }
        public long InvoiceId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? Amount { get; set; }
        public long? PurchaseOrderDetailId { get; set; }
        public long? SalesOrderDetailId { get; set; }
        public long ItemId { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public long? ShipmentDetailId { get; set; }
        public string ExtendedDescription { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public long InvoiceDetailNumber { get; set; }
       

        public string ItemDescription { get; set; }
        public string ItemDescription2 { get; set; }
        public string InvoiceDocument { get; set; }
        public string PONumber { get; set; }

 
    }
    public class InvoiceDetailRepository: Repository<InvoiceDetail>, IInvoiceDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public InvoiceDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
    
 
  public async Task<InvoiceDetail>GetEntityById(long ? invoiceDetailId)
        {
			try{
            return await _dbContext.FindAsync<InvoiceDetail>(invoiceDetailId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<InvoiceDetail> GetEntityByNumber(long invoiceDetailNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.InvoiceDetail
                               where detail.InvoiceDetailNumber == invoiceDetailNumber
                               select detail).FirstOrDefaultAsync<InvoiceDetail>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
        public async Task<IList<InvoiceDetail>> GetEntitiesByInvoiceId(long? invoiceId)
        {
            IList<InvoiceDetail> list = await (from detail in _dbContext.InvoiceDetail
                                               where detail.InvoiceId == invoiceId
                                               select detail
                                             ).ToListAsync<InvoiceDetail>();
            return list;
        }

        
		public async Task<InvoiceDetail> FindEntityByExpression(Expression<Func<InvoiceDetail, bool>> predicate)
        {
            IQueryable<InvoiceDetail> result = _dbContext.Set<InvoiceDetail>().Where(predicate);

            return await result.FirstOrDefaultAsync<InvoiceDetail>();
        }
		
  }
}
