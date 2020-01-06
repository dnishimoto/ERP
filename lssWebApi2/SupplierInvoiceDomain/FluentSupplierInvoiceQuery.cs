using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.SupplierInvoiceDetailDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.SupplierInvoiceDomain
{
public class FluentSupplierInvoiceQuery: MapperAbstract<SupplierInvoice, SupplierInvoiceView>, IFluentSupplierInvoiceQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSupplierInvoiceQuery() { }
        public FluentSupplierInvoiceQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<SupplierInvoice> MapToEntity(SupplierInvoiceView inputObject)
        {

            SupplierInvoice outObject = mapper.Map<SupplierInvoice>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<SupplierInvoice>> MapToEntity(IList<SupplierInvoiceView> inputObjects)
        {
            IList<SupplierInvoice> list = new List<SupplierInvoice>();

            foreach (var item in inputObjects)
            {
                SupplierInvoice outObject = mapper.Map<SupplierInvoice>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        private  async Task<SupplierInvoiceDetailView> MapToDetailView(SupplierInvoiceDetail inputObject)
        {

            SupplierInvoiceDetailView outObject = mapper.Map<SupplierInvoiceDetailView>(inputObject);
                         

            await Task.Yield();
            return outObject;
        }
        private async Task<IList<SupplierInvoiceDetailView>> MapToSupplierDetailViews(IList<SupplierInvoiceDetail> inputList)
        {
            IList<SupplierInvoiceDetailView> list = new List<SupplierInvoiceDetailView>();
            foreach (var item in inputList)
            {
                list.Add(await MapToDetailView(item));
            }
            return list;
        }
 public override async Task<SupplierInvoiceView> MapToView(SupplierInvoice inputObject)
        {

            SupplierInvoiceView outObject = mapper.Map<SupplierInvoiceView>(inputObject);

            outObject.SupplierInvoiceDetailViews = await MapToSupplierDetailViews(await _unitOfWork.supplierInvoiceDetailRepository.getEntitiesByInvoiceId(inputObject.SupplierInvoiceId));

            Task<PurchaseOrder> purchaseOrderTask =  _unitOfWork.purchaseOrderRepository.GetEntityById(inputObject.PurchaseOrderId);
            Task<Invoice> invoiceTask =  _unitOfWork.invoiceRepository.GetEntityById(inputObject.InvoiceId);
            Task<Supplier> supplierTask =  _unitOfWork.supplierRepository.GetEntityById(inputObject.SupplierId);

            Task.WaitAll(purchaseOrderTask, invoiceTask, supplierTask);

            AddressBook addressBook = await _unitOfWork.addressBookRepository.GetEntityById(supplierTask.Result?.AddressId);

            outObject.SupplierName = addressBook?.Name;
            outObject.PurchaseOrderId = purchaseOrderTask.Result.PurchaseOrderId;
            outObject.PONumber = purchaseOrderTask.Result.Ponumber;
            outObject.InvoiceDocument = invoiceTask.Result?.InvoiceDocument;

            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.supplierInvoiceRepository.GetNextNumber(TypeOfSupplierInvoice.SupplierInvoiceNumber.ToString());
        }
 public override async Task<SupplierInvoiceView> GetViewById(long ? supplierInvoiceId)
        {
            SupplierInvoice detailItem = await _unitOfWork.supplierInvoiceRepository.GetEntityById(supplierInvoiceId);

            return await MapToView(detailItem);
        }
 public async Task<SupplierInvoiceView> GetViewByNumber(long supplierInvoiceNumber)
        {
            SupplierInvoice detailItem = await _unitOfWork.supplierInvoiceRepository.GetEntityByNumber(supplierInvoiceNumber);

            return await MapToView(detailItem);
        }

public override async Task<SupplierInvoice> GetEntityById(long ? supplierInvoiceId)
        {
            return await _unitOfWork.supplierInvoiceRepository.GetEntityById(supplierInvoiceId);

        }
 public async Task<SupplierInvoice> GetEntityByNumber(long supplierInvoiceNumber)
        {
            return await _unitOfWork.supplierInvoiceRepository.GetEntityByNumber(supplierInvoiceNumber);
        }
        public async Task<SupplierInvoice> GetEntityByPONumber(string poNumber)
        {
            return await _unitOfWork.supplierInvoiceRepository.GetEntityByPONumber(poNumber);
        }
}
}
