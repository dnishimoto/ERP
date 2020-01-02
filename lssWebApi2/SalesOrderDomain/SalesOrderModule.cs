using lssWebApi2.FluentAPI;
using lssWebApi2.SalesOrderDetailDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDomain
{
    public class SalesOrderModule
    {
        public FluentSalesOrder SalesOrder = new FluentSalesOrder();
        public FluentSalesOrderDetail SalesOrderDetail = new FluentSalesOrderDetail();
    }
}
