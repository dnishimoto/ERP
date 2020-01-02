using lssWebApi2.AbstractFactory;
using lssWebApi2.PurchaseOrderDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.FluentAPI;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.ItemMasterDomain;

namespace lssWebApi2.PurchaseOrderDetailDomain
{
    public class PurchaseOrderDetailModule : AbstractModule
    {
        public FluentPurchaseOrderDetail PurchaseOrderDetail = new FluentPurchaseOrderDetail();
        public FluentPurchaseOrder PurchaseOrder = new FluentPurchaseOrder();
        public FluentItemMaster ItemMaster = new FluentItemMaster();
    }
}
