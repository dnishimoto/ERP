using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.AddressBookDomain;
using ERP_Core2.Services;
using lssWebApi2.entityframework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.InventoryDomain
{
    public interface IInventorySupplier
    {
        IInventorySupplier CreateSupplierAddressBook(AddressBook addressBook, Emails email);
        IInventorySupplier CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress);
        IInventorySupplier CreateSupplierEmail(long addressId, Emails email);
        IInventorySupplier CreateSupplier(Supplier supplier);
        IInventorySupplier Apply();
        IInventorySupplierQuery Query();
    }
    public interface IInventorySupplierQuery
    {
        AddressBook GetAddressBookbyEmail(Emails email);
        SupplierView GetSupplierViewByEmail(Emails email);
    }
    public class FluentSupplierQuery: AbstractErrorHandling, IInventorySupplierQuery
    {
        protected UnitOfWork _unitOfWork;
        public FluentSupplierQuery() { }
        public FluentSupplierQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public AddressBook GetAddressBookbyEmail(Emails email)
        {
            AddressBook ab = _unitOfWork.supplierRepository.GetAddressBookByEmail(email);
          
            return ab;
        }
        public SupplierView GetSupplierViewByEmail(Emails email)
        {
            Task<SupplierView> resultTask = Task.Run(async () =>await  _unitOfWork.supplierRepository.GetSupplierViewByEmail(email));
            Task.WaitAll(resultTask);
            return resultTask.Result;
        }
    }
    public class FluentSupplier : AbstractErrorHandling, IInventorySupplier
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected CreateProcessStatus processStatus;

        protected FluentSupplierQuery _query = null;
        public IInventorySupplierQuery Query()
        {
            if (_query == null) { _query = new FluentSupplierQuery(unitOfWork); }
            return _query as IInventorySupplierQuery;
        }
        public IInventorySupplier CreateSupplierAddressBook(AddressBook addressBook,  Emails email)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async ()=> await unitOfWork.supplierRepository.CreateSupplierAddressBook(addressBook, email));
            Task.WaitAll(resultTask);

            processStatus = resultTask.Result;
            return this as IInventorySupplier;
        }

        public IInventorySupplier CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async ()=> await unitOfWork.supplierRepository.CreateSupplierLocationAddress(addressId, locationAddress));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IInventorySupplier;
        }
        public IInventorySupplier CreateSupplierEmail(long addressId, Emails email)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async()=> await unitOfWork.supplierRepository.CreateSupplierEmail(addressId, email));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IInventorySupplier;
        }
        public IInventorySupplier CreateSupplier(Supplier supplier)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async ()=> await unitOfWork.supplierRepository.CreateSupplier(supplier));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IInventorySupplier;
        }
        public IInventorySupplier Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IInventorySupplier;
        }

    }

    public class SupplierModule
    {
        public FluentSupplier Supplier = new FluentSupplier();
    }
}
