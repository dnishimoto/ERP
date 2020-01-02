   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace lssWebApi2.PurchaseOrderDetailDomain
{
    public class PurchaseOrderDetailView
    {
      
        public long PurchaseOrderDetailId { get; set; }
        public long PurchaseOrderId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? OrderedQuantity { get; set; }
        public long ItemId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? ReceivedQuantity { get; set; }
        public int? RemainingQuantity { get; set; }
        public string Description { get; set; }
        public long PurchaseOrderDetailNumber { get; set; }

        public string ItemDescription2 { get; set; }
        public string ItemCode { get; set; }
        public string PONumber { get; set; }
    }

    public class PurchaseOrderDetailRepository: Repository<PurchaseOrderDetail>, IPurchaseOrderDetailRepository
    {
        ListensoftwaredbContext _dbContext;
        public PurchaseOrderDetailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<IList<PurchaseOrderDetail>>GetEntitiesByPurchaseOrderId(long ? purchaseOrderId)

        {

            IList<PurchaseOrderDetail> list = await (from e in _dbContext.PurchaseOrderDetail
                               where e.PurchaseOrderId == purchaseOrderId
                               select e).ToListAsync<PurchaseOrderDetail>();
            return list;
        }

        public async Task<PurchaseOrderDetail>GetEntityById(long ?purchaseOrderDetailId)
        {
			try{
            return await _dbContext.FindAsync<PurchaseOrderDetail>(purchaseOrderDetailId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<PurchaseOrderDetail> GetEntityByNumber(long purchaseOrderDetailNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.PurchaseOrderDetail
                               where detail.PurchaseOrderDetailNumber == purchaseOrderDetailNumber
                               select detail).FirstOrDefaultAsync<PurchaseOrderDetail>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<PurchaseOrderDetail> FindEntityByExpression(Expression<Func<PurchaseOrderDetail, bool>> predicate)
        {
            IQueryable<PurchaseOrderDetail> result = _dbContext.Set<PurchaseOrderDetail>().Where(predicate);

            return await result.FirstOrDefaultAsync<PurchaseOrderDetail>();
        }
		
  }
}
