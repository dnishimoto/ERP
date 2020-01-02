   

using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using lssWebApi2.PurchaseOrderDetailDomain;

namespace lssWebApi2.PurchaseOrderDomain
{
    public class PurchaseOrderView
    {
        public long PurchaseOrderId { get; set; }
        public string DocType { get; set; }
        public string PaymentTerms { get; set; }
        public string Remark { get; set; }
        public DateTime? Gldate { get; set; }
        public long AccountId { get; set; }
        public long SupplierId { get; set; }
        public long? ContractId { get; set; }
        public long? PoquoteId { get; set; }
        public string Description { get; set; }
        public string Ponumber { get; set; }
        public string TakenBy { get; set; }
        public long? BuyerId { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? PromisedDeliveredDate { get; set; }
        public decimal? Tax { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? AmountPaid { get; set; }
        public string ShippedToName { get; set; }
        public string ShippedToAddress1 { get; set; }
        public string ShippedToAddress2 { get; set; }
        public string ShippedToCity { get; set; }
        public string ShippedToZipcode { get; set; }
        public string ShippedToState { get; set; }
        public string TaxCode1 { get; set; }
        public string TaxCode2 { get; set; }
        public long PurchaseOrderNumber { get; set; }
        public decimal? Amount { get; set; }
        public string SupplierName { get; set; }

        public string Location { get; set; }
        public string BusUnit { get; set; }
        public string Subsidiary { get; set; }
        public string SubSub { get; set; }
        public string Account { get; set; }
        public string AccountDescription { get; set; }
        public long? CustomerId { get; set; }
        public decimal? QuoteAmount { get; set; }
        public string BuyerName { get; set; }
        public IList<PurchaseOrderDetailView> PurchaseOrderDetailViews { get; set; }
    }
    public class PurchaseOrderRepository: Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        ListensoftwaredbContext _dbContext;
        public PurchaseOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }
        public async Task<PurchaseOrder> GetEntityByOrderNumber(string poNumber)
        {
            try
            {
                PurchaseOrder item = await (from detail in _dbContext.PurchaseOrder
                                            where detail.Ponumber==poNumber
                                            select detail).FirstOrDefaultAsync<PurchaseOrder>();

                return item;
            
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

        public async Task<PurchaseOrder>GetEntityById(long ? purchaseOrderId)
        {
			try{
            return await _dbContext.FindAsync<PurchaseOrder>(purchaseOrderId);
				}
            catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
         public async Task<PurchaseOrder> GetEntityByNumber(long purchaseOrderNumber)
        {
			try
			{
            var query = await (from detail in _dbContext.PurchaseOrder
                               where detail.PurchaseOrderNumber == purchaseOrderNumber
                               select detail).FirstOrDefaultAsync<PurchaseOrder>();
            return query;
			}
			catch (Exception ex) { throw new Exception(GetMyMethodName(), ex); }
        }
		
		public async Task<PurchaseOrder> FindEntityByExpression(Expression<Func<PurchaseOrder, bool>> predicate)
        {
            IQueryable<PurchaseOrder> result = _dbContext.Set<PurchaseOrder>().Where(predicate);

            return await result.FirstOrDefaultAsync<PurchaseOrder>();
        }
		
  }
}
