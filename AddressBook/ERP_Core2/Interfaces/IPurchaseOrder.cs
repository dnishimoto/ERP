﻿using ERP_Core2.PurchaseOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_Core2.Interfaces
{
    public interface IPurchaseOrder
    {
        IPurchaseOrder CreateAcctPayByPurchaseOrderNumber(PurchaseOrderView purchaseOrderView);
        IPurchaseOrder CreatePurchaseOrder(PurchaseOrderView purchaseOrderView);
        IPurchaseOrder CreatePurchaseOrderDetails(PurchaseOrderView purchaseOrderView);
        IPurchaseOrder Apply();
    }
}
