using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.SalesOrderManagementDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderManagementDomain
{
    public class FluentSalesOrderDetailQuery : IFluentSalesOrderDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSalesOrderDetailQuery() { }
        public FluentSalesOrderDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<List<SalesOrderDetail>> MapToSalesOrderDetailEntity(List<SalesOrderDetailView> inputObjects)
        {
            List<SalesOrderDetail> list = new List<SalesOrderDetail>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                SalesOrderDetail outObject = mapper.Map<SalesOrderDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public async Task<SalesOrderDetail> MapToSalesOrderDetailEntity(SalesOrderDetailView inputObject)
        {
            Mapper mapper = new Mapper();
            SalesOrderDetail outObject = mapper.Map<SalesOrderDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<SalesOrderDetailView> MapToSalesOrderDetailView(SalesOrderDetail inputObject)
        {
            Mapper mapper = new Mapper();
            SalesOrderDetailView outObject = mapper.Map<SalesOrderDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<List<SalesOrderDetail>> GetDetailsBySalesOrderId(long salesOrderId)
        {
            return await _unitOfWork.salesOrderDetailRepository.GetDetailsBySalesOrderId(salesOrderId);
        }
        public async Task<List<SalesOrderDetailView>> GetDetailViewsBySalesOrderId(long salesOrderId)
        {
            List<SalesOrderDetailView> listViews = new List<SalesOrderDetailView>();
            List<SalesOrderDetail> list = await _unitOfWork.salesOrderDetailRepository.GetDetailsBySalesOrderId(salesOrderId);
            foreach (var item in list)
            {
                listViews.Add(await MapToSalesOrderDetailView(item));
            }

            return listViews;
       

        }
    }
} 