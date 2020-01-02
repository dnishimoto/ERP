using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.PurchaseOrderDomain
{
public class FluentPurchaseOrderQuery: MapperAbstract<PurchaseOrder,PurchaseOrderView>,IFluentPurchaseOrderQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPurchaseOrderQuery() { }
        public FluentPurchaseOrderQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<PurchaseOrder> MapToEntity(PurchaseOrderView inputObject)
        {
           
            PurchaseOrder outObject = mapper.Map<PurchaseOrder>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<List<PurchaseOrder>> MapToEntity(List<PurchaseOrderView> inputObjects)
        {
            List<PurchaseOrder> list = new List<PurchaseOrder>();
         
            foreach (var item in inputObjects)
            {
                PurchaseOrder outObject = mapper.Map<PurchaseOrder>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<PurchaseOrderView> MapToView(PurchaseOrder inputObject)
        {
        
            PurchaseOrderView outObject = mapper.Map<PurchaseOrderView>(inputObject);
            AddressBook addressBook = null;
            AddressBook buyerAddressBook = null;

            Task<ChartOfAccount> accountTask =  _unitOfWork.chartOfAccountRepository.GetEntityById(inputObject.AccountId);
            Task<Supplier> supplierTask =   _unitOfWork.supplierRepository.GetEntityById(inputObject.SupplierId);
            Task<Contract> contractTask =  _unitOfWork.contractRepository.GetEntityById(inputObject.ContractId??0);
            Task<Poquote> poquoteTask =  _unitOfWork.poQuoteRepository.GetEntityById(inputObject.PoquoteId??0);
            Task<Buyer> buyerTask =  _unitOfWork.buyerRepository.GetEntityById(inputObject.BuyerId??0);
            Task.WaitAll(supplierTask,accountTask, contractTask, poquoteTask, buyerTask);

            if (supplierTask.Result != null) { addressBook = await _unitOfWork.addressBookRepository.GetEntityById(supplierTask.Result.AddressId); }
            if (buyerTask.Result != null) { buyerAddressBook = await _unitOfWork.addressBookRepository.GetEntityByCustomerId(buyerTask.Result.AddressId); }


            outObject.Location = accountTask.Result.Location;
            outObject.BusUnit = accountTask.Result.BusUnit;
            outObject.Subsidiary = accountTask.Result.Subsidiary;
            outObject.SubSub = accountTask.Result.SubSub;
            outObject.Account = accountTask.Result.Account;
            outObject.AccountDescription = accountTask.Result.Description;
            outObject.SupplierName = addressBook?.Name;
            outObject.CustomerId = contractTask.Result?.CustomerId;
            outObject.QuoteAmount = poquoteTask.Result?.QuoteAmount;
            outObject.BuyerName = buyerAddressBook?.Name;
            
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.purchaseOrderRepository.GetNextNumber(TypeOfPurchaseOrder.PurchaseOrderNumber.ToString());
        }
 public override async Task<PurchaseOrderView> GetViewById(long ? purchaseOrderId)
        {
            PurchaseOrder detailItem = await _unitOfWork.purchaseOrderRepository.GetEntityById(purchaseOrderId);

            return await MapToView(detailItem);
        }
 public async Task<PurchaseOrderView> GetViewByNumber(long purchaseOrderNumber)
        {
            PurchaseOrder detailItem = await _unitOfWork.purchaseOrderRepository.GetEntityByNumber(purchaseOrderNumber);

            return await MapToView(detailItem);
        }

public override async Task<PurchaseOrder> GetEntityById(long ? purchaseOrderId)
        {
            return await _unitOfWork.purchaseOrderRepository.GetEntityById(purchaseOrderId);

        }
 public async Task<PurchaseOrder> GetEntityByNumber(long purchaseOrderNumber)
        {
            return await _unitOfWork.purchaseOrderRepository.GetEntityByNumber(purchaseOrderNumber);
        }
}
}
