using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PurchaseOrderDetailDomain
{
public class FluentPurchaseOrderDetailQuery:MapperAbstract<PurchaseOrderDetail,PurchaseOrderDetailView>,IFluentPurchaseOrderDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPurchaseOrderDetailQuery() { }
        public FluentPurchaseOrderDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<PurchaseOrderDetail> MapToEntity(PurchaseOrderDetailView inputObject)
        {
       
            PurchaseOrderDetail outObject = mapper.Map<PurchaseOrderDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<PurchaseOrderDetail>> MapToEntity(IList<PurchaseOrderDetailView> inputObjects)
        {
            IList<PurchaseOrderDetail> list = new List<PurchaseOrderDetail>();

            foreach (var item in inputObjects)
            {
                PurchaseOrderDetail outObject = mapper.Map<PurchaseOrderDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<PurchaseOrderDetailView> MapToView(PurchaseOrderDetail inputObject)
        {

            PurchaseOrderDetailView outObject = mapper.Map<PurchaseOrderDetailView>(inputObject);


            Task<PurchaseOrder> purchaseOrderTask = _unitOfWork.purchaseOrderRepository.GetEntityById(inputObject.PurchaseOrderId);
            Task<Supplier> supplierTask = _unitOfWork.supplierRepository.GetEntityById(inputObject.SupplierId);

            Task.WaitAll(purchaseOrderTask, supplierTask);

            AddressBook addressBookSupplier = await _unitOfWork.addressBookRepository.GetEntityById(supplierTask.Result?.AddressId);

            outObject.PONumber = purchaseOrderTask.Result.Ponumber;
            outObject.SupplierName = addressBookSupplier?.Name;


            await Task.Yield();

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfPurchaseOrderDetail.PurchaseOrderDetailNumber.ToString());
        }
 public override async Task<PurchaseOrderDetailView> GetViewById(long ? purchaseOrderDetailId)
        {
            PurchaseOrderDetail detailItem = await _unitOfWork.purchaseOrderDetailRepository.GetEntityById(purchaseOrderDetailId);

            return await MapToView(detailItem);
        }
 public async Task<PurchaseOrderDetailView> GetViewByNumber(long purchaseOrderDetailNumber)
        {
            PurchaseOrderDetail detailItem = await _unitOfWork.purchaseOrderDetailRepository.GetEntityByNumber(purchaseOrderDetailNumber);

            return await MapToView(detailItem);
        }

public override async Task<PurchaseOrderDetail> GetEntityById(long ? purchaseOrderDetailId)
        {
            return await _unitOfWork.purchaseOrderDetailRepository.GetEntityById(purchaseOrderDetailId);

        }
 public async Task<PurchaseOrderDetail> GetEntityByNumber(long purchaseOrderDetailNumber)
        {
            return await _unitOfWork.purchaseOrderDetailRepository.GetEntityByNumber(purchaseOrderDetailNumber);
        }
        public async Task<IList<PurchaseOrderDetail>> GetEntitiesByPurchaseOrderId(long? purchaseOrderId)
        {
            return await _unitOfWork.purchaseOrderDetailRepository.GetEntitiesByPurchaseOrderId(purchaseOrderId);
        }
}
}
