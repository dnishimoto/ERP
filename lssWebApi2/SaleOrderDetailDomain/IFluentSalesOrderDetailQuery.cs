using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDetailDomain
{
    public interface IFluentSalesOrderDetailQuery
    {
        Task<SalesOrderDetail> MapToEntity(SalesOrderDetailView inputObject);
        Task<List<SalesOrderDetail>> MapToEntity(List<SalesOrderDetailView> inputObjects);
        Task<IList<SalesOrderDetail>> GetDetailsBySalesOrderId(long ? salesOrderId);
        Task<IList<SalesOrderDetailView>> GetDetailViewsBySalesOrderId(long? salesOrderId);
        Task<SalesOrderDetailView> MapToView(SalesOrderDetail inputObject);
        Task<NextNumber> GetNextNumber();
        Task<SalesOrderDetailView> GetViewById(long? salesOrderDetailId);
        Task<SalesOrderDetailView> GetViewByNumber(long salesOrderDetailNumber);
        Task<SalesOrderDetail> GetEntityById(long ? salesOrderDetailId);
        Task<SalesOrderDetail> GetEntityByNumber(long salesOrderDetailNumber);
    }
}
