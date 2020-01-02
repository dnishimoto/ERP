using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.SupplierDomain
{
public class FluentSupplierQuery:MapperAbstract<Supplier,SupplierView>,IFluentSupplierQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSupplierQuery() { }
        public FluentSupplierQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

 public override async Task<Supplier> MapToEntity(SupplierView inputObject)
        {
   
            Supplier outObject = mapper.Map<Supplier>(inputObject);
            await Task.Yield();
            return outObject;
        }

  public override async Task<List<Supplier>> MapToEntity(List<SupplierView> inputObjects)
        {
            List<Supplier> list = new List<Supplier>();

            foreach (var item in inputObjects)
            {
                Supplier outObject = mapper.Map<Supplier>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

 public override async Task<SupplierView> MapToView(Supplier inputObject)
        {
   
            SupplierView outObject = mapper.Map<SupplierView>(inputObject);

            AddressBook addressBook = await _unitOfWork.addressBookRepository.GetEntityById(inputObject.AddressId);
            outObject.SupplierName = addressBook.Name;

            await Task.Yield();
            return outObject;
        }

        public async Task<SupplierView> GetSupplierBySupplierId(long supplierId)
        {

            try
            {
                SupplierView result = await MapToView(await _unitOfWork.supplierRepository.GetEntityById(supplierId));

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("GetSupplierBySupplierId", ex);
            }

        }
        public async Task<NextNumber>GetNextNumber()
        {
            return await _unitOfWork.supplierRepository.GetNextNumber(TypeOfSupplier.SupplierNumber.ToString());
        }
 public override  async Task<SupplierView> GetViewById(long ? supplierId)
        {
            Supplier detailItem = await _unitOfWork.supplierRepository.GetEntityById(supplierId);

            return await MapToView(detailItem);
        }
        public async Task<SupplierView> GetViewByNumber(long supplierNumber)
        {
            Supplier detailItem = await _unitOfWork.supplierRepository.GetEntityByNumber(supplierNumber);

            return await MapToView(detailItem);
        }

public override async Task<Supplier> GetEntityById(long ? supplierId)
        {
            return await _unitOfWork.supplierRepository.GetEntityById(supplierId);

        }
 public async Task<Supplier> GetEntityByNumber(long supplierNumber)
        {
            return await _unitOfWork.supplierRepository.GetEntityByNumber(supplierNumber);
        }
        public async Task<SupplierView> GetViewByEmail(EmailEntity emailEntity)
        {
            return await MapToView(await _unitOfWork.supplierRepository.GetEntityByEmail(emailEntity));
        }
}
}
