using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.POQuoteDomain
{
public class FluentPOQuoteQuery:MapperAbstract<Poquote,POQuoteView>,IFluentPOQuoteQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentPOQuoteQuery() { }
        public FluentPOQuoteQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Poquote> MapToEntity(POQuoteView inputObject)
        {
     
            Poquote outObject = mapper.Map<Poquote>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<Poquote>> MapToEntity(IList<POQuoteView> inputObjects)
        {
            IList<Poquote> list = new List<Poquote>();

            foreach (var item in inputObjects)
            {
                Poquote outObject = mapper.Map<Poquote>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<POQuoteView> MapToView(Poquote inputObject)
        {
         
            POQuoteView outObject = mapper.Map<POQuoteView>(inputObject);


            Task<PurchaseOrder> purchaseOrderTask = _unitOfWork.purchaseOrderRepository.GetEntityById(inputObject.PurchaseOrderId);
            Task<Customer> customerTask =  _unitOfWork.customerRepository.GetEntityById(inputObject.CustomerId);
            Task<Supplier> supplierTask =  _unitOfWork.supplierRepository.GetEntityById(inputObject.SupplierId);
            Task.WaitAll(purchaseOrderTask, customerTask, supplierTask);

            Task<AddressBook>customerAddressTask = _unitOfWork.addressBookRepository.GetEntityById(customerTask.Result?.AddressId);
            Task<AddressBook>supplierAddressTask =  _unitOfWork.addressBookRepository.GetEntityById(supplierTask.Result?.AddressId);
            Task.WaitAll(customerAddressTask, supplierAddressTask);

            outObject.SupplierName = supplierAddressTask.Result?.Name;
            outObject.CustomerName = customerAddressTask.Result?.Name;

            await Task.Yield();

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.poQuoteRepository.GetNextNumber(TypeOfPOQuote.PoquoteNumber.ToString());
        }
 public override async Task<POQuoteView> GetViewById(long ? poQuoteId)
        {
            Poquote detailItem = await _unitOfWork.poQuoteRepository.GetEntityById(poQuoteId);

            return await MapToView(detailItem);
        }
 public async Task<POQuoteView> GetViewByNumber(long poQuoteNumber)
        {
            Poquote detailItem = await _unitOfWork.poQuoteRepository.GetEntityByNumber(poQuoteNumber);

            return await MapToView(detailItem);
        }

public override async Task<Poquote> GetEntityById(long ? poQuoteId)
        {
            return await _unitOfWork.poQuoteRepository.GetEntityById(poQuoteId);

        }
 public async Task<Poquote> GetEntityByNumber(long poQuoteNumber)
        {
            return await _unitOfWork.poQuoteRepository.GetEntityByNumber(poQuoteNumber);
        }
}
}
