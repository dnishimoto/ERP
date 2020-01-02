using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPurchaseOrderQuery
{
    Task<PurchaseOrder> MapToEntity(PurchaseOrderView inputObject);
    Task<List<PurchaseOrder>> MapToEntity(List<PurchaseOrderView> inputObjects);
    Task<PurchaseOrderView> MapToView(PurchaseOrder inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PurchaseOrder> GetEntityById(long ? purchaseOrderId);
    Task<PurchaseOrder> GetEntityByNumber(long purchaseOrderNumber);
    Task<PurchaseOrderView> GetViewById(long ? purchaseOrderId);
    Task<PurchaseOrderView> GetViewByNumber(long purchaseOrderNumber);
}
