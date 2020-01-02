

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.PurchaseOrderDetailDomain
{
public interface IPurchaseOrderDetailRepository
    {
        Task<PurchaseOrderDetail> GetEntityById(long ? purchaseOrderDetailId);
		Task<PurchaseOrderDetail> FindEntityByExpression(Expression<Func<PurchaseOrderDetail, bool>> predicate);
        Task<IList<PurchaseOrderDetail>> GetEntitiesByPurchaseOrderId(long ? purchaseOrderId);
    }
}
