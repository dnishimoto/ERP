using ERP_Core2.TaxRatesByCodeDomain;
using lssWebApi2.FluentAPI;
using lssWebApi2.SalesOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentsModule
    {
        public FluentShipments Shipments = new FluentShipments();
        public FluentShipmentsDetail ShipmentsDetail = new FluentShipmentsDetail();
        public FluentSalesOrderDetail SalesOrderDetail = new FluentSalesOrderDetail();
        public FluentSalesOrder SalesOrder = new FluentSalesOrder();
        public FluentTaxRatesByCode TaxRatesByCode = new FluentTaxRatesByCode();
    }
}
