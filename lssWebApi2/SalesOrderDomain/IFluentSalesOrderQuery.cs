using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using lssWebApi2.SalesOrderDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentSalesOrderQuery
    {
        Task<Udc> GetUdc(string productCode, string keyCode);
        Task<NextNumber> GetNextNumber();
        Task<SalesOrder> MapToEntity(SalesOrderView inputObject);
        Task<SalesOrder> GetEntityByNumber(string orderNumber);
        Task<SalesOrderView> GetViewById(long salesOrderId);
        Task<SalesOrder> GetEntityById(long salesOrderId);
        Task<SalesOrderView> MapToView(SalesOrder inputObject);
        SalesOrder SumAmounts(SalesOrder salesOrder, List<SalesOrderDetail> salesOrderDetails);
        Task<PageListViewContainer<SalesOrderView>> GetViewsByPage(Func<SalesOrder, bool> predicate, Func<SalesOrder, object> order, int pageSize, int pageNumber);
    }
}
