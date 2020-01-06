using lssWebApi2.AutoMapper;
using lssWebApi2.PurchaseOrderDetailDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPurchaseOrderDetailQuery
{
    Task<PurchaseOrderDetail> MapToEntity(PurchaseOrderDetailView inputObject);
    Task<IList<PurchaseOrderDetail>> MapToEntity(IList<PurchaseOrderDetailView> inputObjects);
    Task<PurchaseOrderDetailView> MapToView(PurchaseOrderDetail inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PurchaseOrderDetail> GetEntityById(long ? purchaseOrderDetailId);
    Task<PurchaseOrderDetail> GetEntityByNumber(long purchaseOrderDetailNumber);
    Task<PurchaseOrderDetailView> GetViewById(long ? purchaseOrderDetailId);
    Task<PurchaseOrderDetailView> GetViewByNumber(long purchaseOrderDetailNumber);
}
