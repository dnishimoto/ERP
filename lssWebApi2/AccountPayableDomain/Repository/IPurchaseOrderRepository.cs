using ERP_Core2.AccountPayableDomain;
using ERP_Core2.PurchaseOrderDomain;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.AccountPayableDomain.Repository
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrderView> GetPurchaseOrderViewByOrderNumber(string orderNumber);
        Task<CreateProcessStatus> CreatePurchaseOrderByView(PurchaseOrderView purchaseOrderView);
        Task<CreateProcessStatus> CreatePurchaseOrderDetailsByView(PurchaseOrderView purchaseOrderView);
        Task<PurchaseOrder> GetPurchaseOrderByDocNumber(string PONumber);
    }
}
