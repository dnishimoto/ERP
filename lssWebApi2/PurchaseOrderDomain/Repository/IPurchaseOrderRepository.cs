

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.PurchaseOrderDomain
{
public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder> GetEntityById(long ? purchaseOrderId);
	    Task<PurchaseOrder> FindEntityByExpression(Expression<Func<PurchaseOrder, bool>> predicate);
        Task<PurchaseOrder> GetEntityByOrderNumber(string poNumber);
    }
}
