using lssWebApi2.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.SalesOrderDomain;
using lssWebApi2.ItemMasterDomain;
using lssWebApi2.CarrierDomain;
using lssWebApi2.UDCDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.ChartOfAccountsDomain;

namespace lssWebApi2.SalesOrderDetailDomain
{
    public class SalesOrderDetailModule : AbstractModule
    {
        public FluentSalesOrderDetail SalesOrderDetail = new FluentSalesOrderDetail();
        public FluentSalesOrder SalesOrder = new FluentSalesOrder();
        public FluentItemMaster ItemMaster = new FluentItemMaster();
        public FluentChartOfAccount ChartOfAccount = new FluentChartOfAccount();
        public FluentCarrier Carrier = new FluentCarrier();
        public FluentUdc Udc = new FluentUdc();
        public FluentPurchaseOrder purchaseOrder = new FluentPurchaseOrder();
        public FluentPurchaseOrderDetail purchaseOrderDetail = new FluentPurchaseOrderDetail();
    }
}
