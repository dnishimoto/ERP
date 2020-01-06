using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDetailDomain
{
    public interface IFluentSalesOrderDetail
    {
        IFluentSalesOrderDetailQuery Query();
        IFluentSalesOrderDetail Apply();
        IFluentSalesOrderDetail AddSalesOrderDetail(SalesOrderDetail salesOrderDetail);
        IFluentSalesOrderDetail UpdateSalesOrderDetail(SalesOrderDetail salesOrderDetail);
        IFluentSalesOrderDetail DeleteSalesOrderDetail(SalesOrderDetail salesOrderDetail);
        IFluentSalesOrderDetail UpdateSalesOrderDetails(IList<SalesOrderDetail> newObjects);
        IFluentSalesOrderDetail AddSalesOrderDetails(List<SalesOrderDetail> newObjects);
        IFluentSalesOrderDetail DeleteSalesOrderDetails(List<SalesOrderDetail> deleteObjects);
        IFluentSalesOrderDetail UpdateSalesOrderDetailByShipmentsDetail(IEnumerable<ShipmentDetail> shipmentsDetails);
    }
}
