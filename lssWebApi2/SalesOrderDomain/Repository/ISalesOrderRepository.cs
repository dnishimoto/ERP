using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDomain
{
    public interface ISalesOrderRepository
    {
     
        Task<SalesOrder> GetEntityByNumber(string orderNumber);
        Task<SalesOrder> GetEntityById(long ? salesOrderId);
        IQueryable<SalesOrder> GetEntitiesByExpression(Expression<Func<SalesOrder, bool>> predicate);
    }
}
