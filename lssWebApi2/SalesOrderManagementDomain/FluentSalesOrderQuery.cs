using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using lssWebApi2.SalesOrderManagementDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentSalesOrderQuery:IFluentSalesOrderQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSalesOrderQuery() { }
        public FluentSalesOrderQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Udc> GetUdc(string productCode, string keyCode) {
            return await _unitOfWork.salesOrderRepository.GetUdc(productCode, keyCode);
        }
        public async Task<NextNumber> GetSalesOrderNextNumber() {
            return await _unitOfWork.salesOrderRepository.GetNextNumber("SalesOrderNumber");
        }
        public async Task<SalesOrder> MapToSalesOrderEntity(SalesOrderView inputObject)
        {
            Mapper mapper = new Mapper();
            SalesOrder outObject = mapper.Map<SalesOrder>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<SalesOrder> GetSalesOrderByNumber(string orderNumber)
        {
            return await _unitOfWork.salesOrderRepository.GetSalesOrderByNumber(orderNumber);
        }
        public async Task<SalesOrderView> GetSalesOrderViewById(long salesOrderId)
        {
            var salesOrder=await _unitOfWork.salesOrderRepository.GetSalesOrderById(salesOrderId);

            Mapper mapper = new Mapper();
            SalesOrderView view = mapper.Map<SalesOrderView>(salesOrder);

            return view;
        }
        public async Task<SalesOrder> GetSalesOrderById(long salesOrderId)
        {
            return await _unitOfWork.salesOrderRepository.GetSalesOrderById(salesOrderId);

        }

    }
}
