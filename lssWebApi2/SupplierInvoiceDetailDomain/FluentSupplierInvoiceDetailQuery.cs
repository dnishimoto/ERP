using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.SupplierInvoiceDetailDomain
{
public class FluentSupplierInvoiceDetailQuery:MapperAbstract<SupplierInvoiceDetail,SupplierInvoiceDetailView>,IFluentSupplierInvoiceDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSupplierInvoiceDetailQuery() { }
        public FluentSupplierInvoiceDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<SupplierInvoiceDetail> MapToEntity(SupplierInvoiceDetailView inputObject)
        {

            SupplierInvoiceDetail outObject = mapper.Map<SupplierInvoiceDetail>(inputObject);

            await Task.Yield();
            return outObject;
        }

  public override async Task<List<SupplierInvoiceDetail>> MapToEntity(List<SupplierInvoiceDetailView> inputObjects)
        {
            List<SupplierInvoiceDetail> list = new List<SupplierInvoiceDetail>();

            foreach (var item in inputObjects)
            {
                SupplierInvoiceDetail outObject = mapper.Map<SupplierInvoiceDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<SupplierInvoiceDetailView> MapToView(SupplierInvoiceDetail inputObject)
        {

            SupplierInvoiceDetailView outObject = mapper.Map<SupplierInvoiceDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
      
        
  public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.supplierInvoiceDetailRepository.GetNextNumber(TypeOfSupplierInvoiceDetail.SupplierInvoiceDetailNumber.ToString());
        }
 public override async Task<SupplierInvoiceDetailView> GetViewById(long ? supplierInvoiceDetailId)
        {
            SupplierInvoiceDetail detailItem = await _unitOfWork.supplierInvoiceDetailRepository.GetEntityById(supplierInvoiceDetailId);

            return await MapToView(detailItem);
        }
 public async Task<SupplierInvoiceDetailView> GetViewByNumber(long supplierInvoiceDetailNumber)
        {
            SupplierInvoiceDetail detailItem = await _unitOfWork.supplierInvoiceDetailRepository.GetEntityByNumber(supplierInvoiceDetailNumber);

            return await MapToView(detailItem);
        }

public override async Task<SupplierInvoiceDetail> GetEntityById(long ? supplierInvoiceDetailId)
        {
            return await _unitOfWork.supplierInvoiceDetailRepository.GetEntityById(supplierInvoiceDetailId);

        }
 public async Task<SupplierInvoiceDetail> GetEntityByNumber(long supplierInvoiceDetailNumber)
        {
            return await _unitOfWork.supplierInvoiceDetailRepository.GetEntityByNumber(supplierInvoiceDetailNumber);
        }
}
}
