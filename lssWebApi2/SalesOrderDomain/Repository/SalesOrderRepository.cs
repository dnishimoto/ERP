using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using lssWebApi2.SalesOrderDetailDomain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using System.Linq.Expressions;

namespace lssWebApi2.SalesOrderDomain
{
    public class SalesOrderView
    {
        public long SalesOrderId { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? Amount { get; set; }
        public decimal? AmountOpen { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public long CustomerId { get; set; }
        public string TakenBy { get; set; }
        public decimal? FreightAmount { get; set; }
        public string PaymentInstrument { get; set; }
        public string PaymentTerms { get; set; }
        public string Note { get; set; }
        public IList<SalesOrderDetailView> SalesOrderDetailViews { get; set; }
    }
   
    
    public class SalesOrderRepository :  Repository<SalesOrder> , ISalesOrderRepository
    {
        ListensoftwaredbContext _dbContext;
      
        public SalesOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public IQueryable<SalesOrder> GetEntitiesByExpression(Expression<Func<SalesOrder, bool>> predicate)
        {
            //IQueryable<SalesOrder> result = _dbContext.Set<SalesOrder>().Where(predicate).AsQueryable<SalesOrder>();
            var result = _dbContext.Set<SalesOrder>().Where(predicate);
            
            return result;
        }

        public async Task<SalesOrder> GetEntityByNumber(string orderNumber)
        {
            return await _dbContext.SalesOrder.Where(salesOrder=>salesOrder.OrderNumber == orderNumber).FirstOrDefaultAsync<SalesOrder>();
        }
        public async Task<SalesOrder> GetEntityById(long ?salesOrderId)
        {
            return await _dbContext.SalesOrder.FindAsync(salesOrderId);
        }
    }
}
