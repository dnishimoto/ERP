using lssWebApi2.EntityFramework;
using lssWebApi2.SalesOrderManagementDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderManagementDomain
{
    public interface IFluentSalesOrderDetailQuery
    {
        Task<SalesOrderDetail> MapToSalesOrderDetailEntity(SalesOrderDetailView inputObject);
        Task<List<SalesOrderDetail>> MapToSalesOrderDetailEntity(List<SalesOrderDetailView> inputObjects);
        Task<List<SalesOrderDetail>> GetDetailsBySalesOrderId(long salesOrderId);
        Task<List<SalesOrderDetailView>> GetDetailViewsBySalesOrderId(long salesOrderId);
        Task<SalesOrderDetailView> MapToSalesOrderDetailView(SalesOrderDetail inputObject);
    }
}
