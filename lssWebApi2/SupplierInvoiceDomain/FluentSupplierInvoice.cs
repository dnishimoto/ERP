using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.SupplierInvoiceDomain;
using lssWebApi2.Enumerations;
using lssWebApi2.AutoMapper;
using System.Threading.Tasks;

namespace lssWebApi2.SupplierInvoiceDomain
{

public class FluentSupplierInvoice :IFluentSupplierInvoice
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentSupplierInvoice() { }
        public IFluentSupplierInvoiceQuery Query()
        {
            return new FluentSupplierInvoiceQuery(unitOfWork) as IFluentSupplierInvoiceQuery;
        }
        public SupplierInvoice MapToEntity(SupplierInvoiceView inputObject)
        {
            Mapper mapper = new Mapper();
            SupplierInvoice outObject = mapper.Map<SupplierInvoice>(inputObject);
            return outObject;
        }
        public IFluentSupplierInvoice CreateSupplierInvoiceByView(SupplierInvoiceView view)
        {
            decimal amount = 0;
            try
            {
                //check if packing slip exists

                Task<SupplierInvoice> supplierInvoiceLookupTask = Task.Run(async () => await unitOfWork.supplierInvoiceRepository.GetEntityByNumber(view.SupplierInvoiceNumber));
                Task.WaitAll(supplierInvoiceLookupTask);

                if (supplierInvoiceLookupTask.Result != null) { processStatus= CreateProcessStatus.AlreadyExists; return this as IFluentSupplierInvoice; }


                foreach (var detail in view.SupplierInvoiceDetailViews)
                {
                    amount += detail.ExtendedCost ?? 0;
                }
                view.Amount = amount;


                SupplierInvoice supplierInvoice = MapToEntity(view);
               // applicationViewFactory.MapSupplierInvoiceEntity(ref supplierInvoice, view);

                AddSupplierInvoice(supplierInvoice);

                return this as IFluentSupplierInvoice;
            }
            catch (Exception ex) { throw new Exception("CreateSupplierInvoiceByView", ex); }
        }
        public IFluentSupplierInvoice Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentSupplierInvoice;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentSupplierInvoice AddSupplierInvoices(List<SupplierInvoice> newObjects)
        {
            unitOfWork.supplierInvoiceRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplierInvoice;
        }
        public IFluentSupplierInvoice UpdateSupplierInvoices(List<SupplierInvoice> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.supplierInvoiceRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplierInvoice;
        }
        public IFluentSupplierInvoice AddSupplierInvoice(SupplierInvoice newObject) {
            unitOfWork.supplierInvoiceRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentSupplierInvoice;
        }
        public IFluentSupplierInvoice UpdateSupplierInvoice(SupplierInvoice updateObject) {
            unitOfWork.supplierInvoiceRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentSupplierInvoice;

        }
        public IFluentSupplierInvoice DeleteSupplierInvoice(SupplierInvoice deleteObject) {
            unitOfWork.supplierInvoiceRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplierInvoice;
        }
   	public IFluentSupplierInvoice DeleteSupplierInvoices(List<SupplierInvoice> deleteObjects)
        {
            unitOfWork.supplierInvoiceRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentSupplierInvoice;
        }
    }
}
