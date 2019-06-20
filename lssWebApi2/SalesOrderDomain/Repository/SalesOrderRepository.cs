using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using lssWebApi2.SalesOrderDomain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace lssWebApi2.SalesOrderDomain.Repository
{
    public class SalesOrderView
    {
        public SalesOrderView() {
            if (SalesOrderDetailViews == null)
            {
                SalesOrderDetailViews = new List<SalesOrderDetailView>();
            }
        }
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
        public List<SalesOrderDetailView> SalesOrderDetailViews { get; set; }
    }
   
    
    public class SalesOrderRepository :  Repository<SalesOrder> , ISalesOrderRepository
    {
        ListensoftwaredbContext _dbContext;
      
        public SalesOrderRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwaredbContext)db;
        }

        public async Task<SalesOrderView> MapToView(SalesOrder inputObject)
        {
            Mapper mapper = new Mapper();
            SalesOrderView outObject = mapper.Map<SalesOrderView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        private async Task<SalesOrderDetailView> MapToDetailView(SalesOrderDetail inputObject)
        {
            Mapper mapper = new Mapper();
            SalesOrderDetailView outObject = mapper.Map<SalesOrderDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<PageListViewContainer<SalesOrderView>> GetViewsByPage(Func<SalesOrder, bool> predicate, Func<SalesOrder, object> order, int pageSize, int pageNumber)
        {
            try
            {
                //IEnumerable<SalesOrder> query = _dbContext.SalesOrder.Where(predicate).OrderByDescending(order).Select(e => e);
                var query = _dbContext.SalesOrder.Where(predicate).OrderByDescending(order).Select(e => e);

                IPagedList<SalesOrder> list = await query.ToPagedListAsync(pageNumber, pageSize);

                PageListViewContainer<SalesOrderView> container = new PageListViewContainer<SalesOrderView>();
                container.PageNumber = pageNumber;
                container.PageSize = pageSize;
                container.TotalItemCount = list.TotalItemCount;


                foreach (var item in list)
                {
                    SalesOrderView view = await MapToView(item);


                    var query2 = from salesOrderDetail in _dbContext.SalesOrderDetail
                                 where salesOrderDetail.SalesOrderId == item.SalesOrderId
                                 select salesOrderDetail;


                    foreach (var item2 in query2)
                    {
                        view.SalesOrderDetailViews.Add(await MapToDetailView(item2));
                    }


                    container.Items.Add(view);
                }

                //await Task.Yield();
                return container;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }
        public async Task<SalesOrder> GetEntityByNumber(string orderNumber)
        {
            return await _dbContext.SalesOrder.Where(salesOrder=>salesOrder.OrderNumber == orderNumber).FirstOrDefaultAsync<SalesOrder>();
        }
        public async Task<SalesOrder> GetEntityById(long salesOrderId)
        {
            return await _dbContext.SalesOrder.FindAsync(salesOrderId);
        }
    }
}
