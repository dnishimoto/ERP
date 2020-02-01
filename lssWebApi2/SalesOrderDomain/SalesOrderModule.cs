using lssWebApi2.SalesOrderDetailDomain;
using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDomain
{
    public class SalesOrderModule
    {
        private UnitOfWork unitOfWork;
        public FluentSalesOrder SalesOrder;
        public FluentSalesOrderDetail SalesOrderDetail;

        public SalesOrderModule()
        {
            unitOfWork = new UnitOfWork();
            SalesOrder = new FluentSalesOrder(unitOfWork);
            SalesOrderDetail = new FluentSalesOrderDetail(unitOfWork);
        }
    }
}
