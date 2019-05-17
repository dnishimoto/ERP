using lssWebApi2.EntityFramework;
using lssWebApi2.SalesOrderManagementDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface IFluentSalesOrderQuery
    {
        Task<Udc> GetUdc(string productCode, string keyCode);
        Task<NextNumber> GetSalesOrderNextNumber();
        Task<SalesOrder> MapToSalesOrderEntity(SalesOrderView inputObject);
        Task<SalesOrder> GetSalesOrderByNumber(string orderNumber);
        Task<SalesOrderView> GetSalesOrderViewById(long salesOrderId);
        Task<SalesOrder> GetSalesOrderById(long salesOrderId);
    }
}
