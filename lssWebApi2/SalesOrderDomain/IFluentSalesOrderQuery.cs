using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDomain
{
    public interface IFluentSalesOrderQuery
    {
        Task<Udc> GetUdc(string productCode, string keyCode);
        Task<NextNumber> GetNextNumber();
        Task<SalesOrder> MapToEntity(SalesOrderView inputObject);
        Task<IList<SalesOrder>> MapToEntity(IList<SalesOrderView> inputObjects);
        Task<SalesOrder> GetEntityByNumber(string orderNumber);
        Task<SalesOrderView> GetViewById(long ? salesOrderId);
        Task<SalesOrder> GetEntityById(long ? salesOrderId);
        Task<SalesOrderView> MapToView(SalesOrder inputObject);
        SalesOrder SumAmounts(SalesOrder salesOrder, IList<SalesOrderDetail> salesOrderDetails);
        Task<PageListViewContainer<SalesOrderView>> GetViewsByPage(Expression<Func<SalesOrder, bool>> predicate, Expression<Func<SalesOrder, object>> order, int pageSize, int pageNumber);
    }
}
