using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.Interfaces
{
    public interface ISalesOrderDetailRepository
    {
        Task<List<SalesOrderDetail>> GetEntitiesBySalesOrderId(long salesOrderId);
        Task<SalesOrderDetail> GetEntityById(long salesOrderDetailId);
        Task<SalesOrderDetail> GetEntityByNumber(long salesOrderDetailNumber);
    }
}
