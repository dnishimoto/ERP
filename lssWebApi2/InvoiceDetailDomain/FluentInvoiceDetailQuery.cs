using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.InvoiceDetailDomain
{
public class FluentInvoiceDetailQuery:MapperAbstract<InvoiceDetail,InvoiceDetailView>,IFluentInvoiceDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentInvoiceDetailQuery() { }
        public FluentInvoiceDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<InvoiceDetail> MapToEntity(InvoiceDetailView inputObject)
        {

            InvoiceDetail outObject = mapper.Map<InvoiceDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<IList<InvoiceDetail>> MapToEntity(IList<InvoiceDetailView> inputObjects)
        {
            IList<InvoiceDetail> list = new List<InvoiceDetail>();

            foreach (var item in inputObjects)
            {
                InvoiceDetail outObject = mapper.Map<InvoiceDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<InvoiceDetailView> MapToView(InvoiceDetail inputObject)
        {
 
            InvoiceDetailView outObject = mapper.Map<InvoiceDetailView>(inputObject);

       
            Task<ItemMaster> itemMasterTask = _unitOfWork.itemMasterRepository.GetEntityById(inputObject?.ItemId);
            Task<Invoice> invoiceTask =  _unitOfWork.invoiceRepository.GetEntityById(inputObject?.InvoiceId);
            Task.WaitAll(itemMasterTask, invoiceTask);

            outObject.ItemDescription = itemMasterTask.Result?.Description;
            outObject.ItemDescription2 = itemMasterTask.Result?.Description2;
            outObject.InvoiceDocument = invoiceTask.Result?.InvoiceDocument;
                        

            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.invoiceDetailRepository.GetNextNumber(TypeOfInvoiceDetail.InvoiceDetailNumber.ToString());
        }
 public override async Task<InvoiceDetailView> GetViewById(long ? invoiceDetailId)
        {
            InvoiceDetail detailItem = await _unitOfWork.invoiceDetailRepository.GetEntityById(invoiceDetailId);

            return await MapToView(detailItem);
        }
 public async Task<InvoiceDetailView> GetViewByNumber(long invoiceDetailNumber)
        {
            InvoiceDetail detailItem = await _unitOfWork.invoiceDetailRepository.GetEntityByNumber(invoiceDetailNumber);

            return await MapToView(detailItem);
        }

public override async Task<InvoiceDetail> GetEntityById(long ? invoiceDetailId)
        {
            return await _unitOfWork.invoiceDetailRepository.GetEntityById(invoiceDetailId);

        }
 public async Task<InvoiceDetail> GetEntityByNumber(long invoiceDetailNumber)
        {
            return await _unitOfWork.invoiceDetailRepository.GetEntityByNumber(invoiceDetailNumber);
        }
}
}
