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
using lssWebApi2.Services;

namespace lssWebApi2.SalesOrderDetailDomain
{
    public class SalesOrderDetailModule : AbstractModule
    {
        private UnitOfWork unitOfWork;
        public FluentSalesOrderDetail SalesOrderDetail;
        public FluentSalesOrder SalesOrder;
        public FluentItemMaster ItemMaster;
        public FluentChartOfAccount ChartOfAccount;
        public FluentCarrier Carrier;
        public FluentUdc Udc;
        public FluentPurchaseOrder purchaseOrder;
        public FluentPurchaseOrderDetail purchaseOrderDetail;

        public SalesOrderDetailModule()
        {
            unitOfWork = new UnitOfWork();

            SalesOrderDetail = new FluentSalesOrderDetail(unitOfWork);
            SalesOrder = new FluentSalesOrder(unitOfWork);
            ItemMaster = new FluentItemMaster(unitOfWork);
            ChartOfAccount = new FluentChartOfAccount(unitOfWork);
            Carrier = new FluentCarrier(unitOfWork);
            Udc = new FluentUdc(unitOfWork);
            purchaseOrder = new FluentPurchaseOrder(unitOfWork);
            purchaseOrderDetail = new FluentPurchaseOrderDetail(unitOfWork);
        }
    }
}
