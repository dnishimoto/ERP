using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using lssWebApi2.SalesOrderDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace lssWebApi2.SalesOrderDomain
{
    public class FluentSalesOrderQuery:MapperAbstract<SalesOrder, SalesOrderView>,IFluentSalesOrderQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSalesOrderQuery() { }
        public FluentSalesOrderQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Udc> GetUdc(string productCode, string keyCode) {
            return await _unitOfWork.udcRepository.GetUdc(productCode, keyCode);
        }
      
        public async Task<PageListViewContainer<SalesOrderView>> GetViewsByPage(Expression<Func<SalesOrder, bool>> predicate, Expression<Func<SalesOrder, object>> order, int pageSize, int pageNumber)
        {
            try
            {
                //IEnumerable<SalesOrder> query = _dbContext.SalesOrder.Where(predicate).OrderByDescending(order).Select(e => e);
                var query =  _unitOfWork.salesOrderRepository.GetEntitiesByExpression(predicate);
                query=query.OrderByDescending(order).Select(e => e);

                IPagedList<SalesOrder> list = await query.ToPagedListAsync(pageNumber, pageSize);

                PageListViewContainer<SalesOrderView> container = new PageListViewContainer<SalesOrderView>();
                container.PageNumber = pageNumber;
                container.PageSize = pageSize;
                container.TotalItemCount = list.TotalItemCount;


                foreach (var item in list)
                {
                    SalesOrderView view = await MapToView(item);


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
        public SalesOrder SumAmounts(SalesOrder salesOrder,IList<SalesOrderDetail> salesOrderDetails)
        {
            decimal? amount = salesOrderDetails.Sum(e => e.Amount);
            decimal? amountOpen = salesOrderDetails.Sum(e => e.AmountOpen);

            salesOrder.Amount = amount;
            salesOrder.AmountOpen = amountOpen;

            return salesOrder;
        }
        public async Task<NextNumber> GetNextNumber() {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfSalesOrder.SalesOrderNumber.ToString());
        }
        public override async Task<SalesOrder> MapToEntity(SalesOrderView inputObject)
        {

            SalesOrder outObject = mapper.Map<SalesOrder>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public override async Task<IList<SalesOrder>> MapToEntity(IList<SalesOrderView> inputObjects)
        {
            IList<SalesOrder> list = new List<SalesOrder>();

            foreach (var item in inputObjects)
            {
                SalesOrder outObject = mapper.Map<SalesOrder>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public override async Task<SalesOrderView> MapToView(SalesOrder inputObject)
        {
          
            SalesOrderView outObject = mapper.Map<SalesOrderView>(inputObject);


            IList<SalesOrderDetail> list = await _unitOfWork.salesOrderDetailRepository.GetEntitiesBySalesOrderId(inputObject.SalesOrderId);
            List<SalesOrderDetailView> viewsList = new List<SalesOrderDetailView>();
            foreach (var item in list)
            {
                viewsList.Add(await MapToDetailView(item));
            }

            outObject.SalesOrderDetailViews = viewsList;

            return outObject;
        }
        private async Task<SalesOrderDetailView> MapToDetailView(SalesOrderDetail inputObject)
        {

            SalesOrderDetailView outObject = mapper.Map<SalesOrderDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
       
       
    
        public async Task<SalesOrder> GetEntityByNumber(string orderNumber)
        {
            return await _unitOfWork.salesOrderRepository.GetEntityByNumber(orderNumber);
        }
        public override async Task<SalesOrderView> GetViewById(long ? salesOrderId)
        {
            var salesOrder=await _unitOfWork.salesOrderRepository.GetEntityById(salesOrderId);
            return await MapToView(salesOrder);

        }
        public override async Task<SalesOrder> GetEntityById(long ? salesOrderId)
        {
            return await _unitOfWork.salesOrderRepository.GetEntityById(salesOrderId);

        }

    }
}
