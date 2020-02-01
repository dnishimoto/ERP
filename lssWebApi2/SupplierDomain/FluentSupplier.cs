using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;
using System.Threading.Tasks;
using lssWebApi2.AutoMapper;
using lssWebApi2.AbstractFactory;

namespace lssWebApi2.SupplierDomain
{

public class FluentSupplier :IFluentSupplier
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();

        public FluentSupplier(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }


        public IFluentSupplierQuery Query()
        {
            return new FluentSupplierQuery(unitOfWork) as IFluentSupplierQuery;
        }
       

        public IFluentSupplier Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentSupplier;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
    
        public IFluentSupplier CreateSupplier(Supplier supplier)

        {
            Task<Supplier> supplierLookupTask = Task.Run(async () => await unitOfWork.supplierRepository.FindEntityByAddressId(supplier.AddressId));
            Task.WaitAll(supplierLookupTask);

            if (supplierLookupTask.Result == null)
            {
                AddSupplier(supplier);
                return this as IFluentSupplier;
            }
            processStatus= CreateProcessStatus.AlreadyExists;
            return this as IFluentSupplier;

        }
        public IFluentSupplier AddSuppliers(List<Supplier> newObjects)
        {
            unitOfWork.supplierRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplier;
        }
        public IFluentSupplier UpdateSuppliers(IList<Supplier> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.supplierRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplier;
        }
        public IFluentSupplier AddSupplier(Supplier newObject) {
            unitOfWork.supplierRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplier;
        }
        public IFluentSupplier UpdateSupplier(Supplier updateObject) {
            unitOfWork.supplierRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplier;

        }
        public IFluentSupplier DeleteSupplier(Supplier deleteObject) {
            unitOfWork.supplierRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplier;
        }
   	public IFluentSupplier DeleteSuppliers(List<Supplier> deleteObjects)
        {
            unitOfWork.supplierRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplier;
        }
    }
}
