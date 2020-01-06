

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.PurchaseOrderDomain
{ 

public interface IFluentPurchaseOrder
    {
        IFluentPurchaseOrderQuery Query();
        IFluentPurchaseOrder Apply();
        IFluentPurchaseOrder AddPurchaseOrder(PurchaseOrder purchaseOrder);
        IFluentPurchaseOrder UpdatePurchaseOrder(PurchaseOrder purchaseOrder);
        IFluentPurchaseOrder DeletePurchaseOrder(PurchaseOrder purchaseOrder);
     	IFluentPurchaseOrder UpdatePurchaseOrders(IList<PurchaseOrder> newObjects);
        IFluentPurchaseOrder AddPurchaseOrders(List<PurchaseOrder> newObjects);
        IFluentPurchaseOrder DeletePurchaseOrders(List<PurchaseOrder> deleteObjects);
        IFluentPurchaseOrder CreatePurchaseOrderByView(PurchaseOrderView purchaseOrderView);
    }
}
