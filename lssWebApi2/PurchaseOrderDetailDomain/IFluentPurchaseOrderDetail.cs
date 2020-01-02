

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.PurchaseOrderDetailDomain
{ 

public interface IFluentPurchaseOrderDetail
    {
        IFluentPurchaseOrderDetailQuery Query();
        IFluentPurchaseOrderDetail Apply();
        IFluentPurchaseOrderDetail AddPurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail);
        IFluentPurchaseOrderDetail UpdatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail);
        IFluentPurchaseOrderDetail DeletePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail);
     	IFluentPurchaseOrderDetail UpdatePurchaseOrderDetails(List<PurchaseOrderDetail> newObjects);
        IFluentPurchaseOrderDetail AddPurchaseOrderDetails(List<PurchaseOrderDetail> newObjects);
        IFluentPurchaseOrderDetail DeletePurchaseOrderDetails(List<PurchaseOrderDetail> deleteObjects);
        IFluentPurchaseOrderDetail CreatePurchaseOrderDetailsByView(PurchaseOrderView purchaseOrderView);
    }
}
