using ERP_Core2.PurchaseOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IFluentPurchaseOrder
    {
        IFluentPurchaseOrder CreateAcctPayByPurchaseOrderNumber(PurchaseOrderView purchaseOrderView);
        IFluentPurchaseOrder CreatePurchaseOrder(PurchaseOrderView purchaseOrderView);
        IFluentPurchaseOrder CreatePurchaseOrderDetails(PurchaseOrderView purchaseOrderView);
        IFluentPurchaseOrder Apply();
    }
}
