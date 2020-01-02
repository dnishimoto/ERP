using lssWebApi2.AbstractFactory;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.SupplierDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public class FluentInventorySupplierQuery : AbstractErrorHandling, IFluentInventorySupplierQuery
    {
        protected UnitOfWork _unitOfWork;
        public FluentInventorySupplierQuery() { }
        public FluentInventorySupplierQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<AddressBook> GetAddressBookbyEmail(EmailEntity email)
        {
            AddressBook ab = await _unitOfWork.addressBookRepository.GetEntityByAccountEmail(email.Email);

            return ab;
        }
        private async Task<SupplierView> MapToSupplierView(Supplier inputObject)
        {
            Mapper mapper = new Mapper();
            SupplierView outObject = mapper.Map<SupplierView>(inputObject);

            AddressBook addressBook = await _unitOfWork.addressBookRepository.GetEntityById(inputObject.AddressId);
            outObject.SupplierName = addressBook.Name;

            await Task.Yield();
            return outObject;
        }
        public async Task<SupplierView> GetSupplierViewByEmail(EmailEntity emailEntity)
        {
            Supplier supplier =  await _unitOfWork.supplierRepository.GetEntityByEmail(emailEntity);
            SupplierView view = await MapToSupplierView(supplier);
            return view;
        }
    }
}
