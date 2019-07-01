using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDomain
{
    public class FluentSalesOrderQuery:IFluentSalesOrderQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSalesOrderQuery() { }
        public FluentSalesOrderQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Udc> GetUdc(string productCode, string keyCode) {
            return await _unitOfWork.salesOrderRepository.GetUdc(productCode, keyCode);
        }
        public async Task<PageListViewContainer<SalesOrderView>> GetViewsByPage(Func<SalesOrder, bool> predicate, Func<SalesOrder, object> order, int pageSize, int pageNumber)
        {
            return await _unitOfWork.salesOrderRepository.GetViewsByPage(predicate, order, pageSize, pageNumber);
        }
        public SalesOrder SumAmounts(SalesOrder salesOrder,List<SalesOrderDetail> salesOrderDetails)
        {
            decimal? amount = salesOrderDetails.Sum(e => e.Amount);
            decimal? amountOpen = salesOrderDetails.Sum(e => e.AmountOpen);

            salesOrder.Amount = amount;
            salesOrder.AmountOpen = amountOpen;

            return salesOrder;
        }
        public async Task<NextNumber> GetNextNumber() {
            return await _unitOfWork.salesOrderRepository.GetNextNumber(TypeOfNextNumberEnum.SalesOrderNumber.ToString());
        }
        public async Task<SalesOrderView> MapToView(SalesOrder inputObject)
        {
            return await _unitOfWork.salesOrderRepository.MapToView(inputObject);
        }
        public async Task<SalesOrder> MapToEntity(SalesOrderView inputObject)
        {
            Mapper mapper = new Mapper();
            SalesOrder outObject = mapper.Map<SalesOrder>(inputObject);
            await Task.Yield();
            return outObject;
        }
    
        public async Task<SalesOrder> GetEntityByNumber(string orderNumber)
        {
            return await _unitOfWork.salesOrderRepository.GetEntityByNumber(orderNumber);
        }
        public async Task<SalesOrderView> GetViewById(long salesOrderId)
        {
            var salesOrder=await _unitOfWork.salesOrderRepository.GetEntityById(salesOrderId);

            Mapper mapper = new Mapper();
            SalesOrderView view = mapper.Map<SalesOrderView>(salesOrder);

            return view;
        }
        public async Task<SalesOrder> GetEntityById(long salesOrderId)
        {
            return await _unitOfWork.salesOrderRepository.GetEntityById(salesOrderId);

        }

    }
}
