using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDomain
{
    public interface ISalesOrderRepository
    {
        Task<SalesOrderView> MapToView(SalesOrder inputObject);
        Task<PageListViewContainer<SalesOrderView>> GetViewsByPage(Func<SalesOrder, bool> predicate, Func<SalesOrder, object> order, int pageSize, int pageNumber);
        Task<SalesOrder> GetEntityByNumber(string orderNumber);
        Task<SalesOrder> GetEntityById(long salesOrderId);
    }
}
