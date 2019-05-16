using ERP_Core2.AbstractFactory;
using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.FluentAPI
{
    public class FluentInventorySupplier : AbstractErrorHandling, IFluentInventorySupplier
    {
        protected UnitOfWork unitOfWork = new UnitOfWork();

        protected CreateProcessStatus processStatus;

        protected FluentInventorySupplierQuery _query = null;
        public IFluentInventorySupplierQuery Query()
        {
            if (_query == null) { _query = new FluentInventorySupplierQuery(unitOfWork); }
            return _query as IFluentInventorySupplierQuery;
        }
        public IFluentInventorySupplier CreateSupplierAddressBook(AddressBook addressBook, Emails email)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async () => await unitOfWork.supplierRepository.CreateSupplierAddressBook(addressBook, email));
            Task.WaitAll(resultTask);

            processStatus = resultTask.Result;
            return this as IFluentInventorySupplier;
        }

        public IFluentInventorySupplier CreateSupplierLocationAddress(long addressId, LocationAddress locationAddress)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async () => await unitOfWork.supplierRepository.CreateSupplierLocationAddress(addressId, locationAddress));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IFluentInventorySupplier;
        }
        public IFluentInventorySupplier CreateSupplierEmail(long addressId, Emails email)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async () => await unitOfWork.supplierRepository.CreateSupplierEmail(addressId, email));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IFluentInventorySupplier;
        }
        public IFluentInventorySupplier CreateSupplier(Supplier supplier)
        {
            Task<CreateProcessStatus> resultTask = Task.Run(async () => await unitOfWork.supplierRepository.CreateSupplier(supplier));
            Task.WaitAll(resultTask);
            processStatus = resultTask.Result;
            return this as IFluentInventorySupplier;
        }
        public IFluentInventorySupplier Apply()
        {
            if (processStatus == CreateProcessStatus.Insert || processStatus == CreateProcessStatus.Update || processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentInventorySupplier;
        }

    }
}
