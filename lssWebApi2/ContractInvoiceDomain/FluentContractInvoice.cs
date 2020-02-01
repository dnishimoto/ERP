using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ContractInvoiceDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ContractInvoiceDomain
{

public class FluentContractInvoice :IFluentContractInvoice
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentContractInvoice(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentContractInvoiceQuery Query()
        {
            return new FluentContractInvoiceQuery(unitOfWork) as IFluentContractInvoiceQuery;
        }
        public IFluentContractInvoice Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentContractInvoice;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentContractInvoice AddContractInvoices(List<ContractInvoice> newObjects)
        {
            unitOfWork.contractInvoiceRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentContractInvoice;
        }
        public IFluentContractInvoice UpdateContractInvoices(IList<ContractInvoice> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.contractInvoiceRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentContractInvoice;
        }
        public IFluentContractInvoice AddContractInvoice(ContractInvoice newObject) {
            unitOfWork.contractInvoiceRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentContractInvoice;
        }
        public IFluentContractInvoice UpdateContractInvoice(ContractInvoice updateObject) {
            unitOfWork.contractInvoiceRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentContractInvoice;

        }
        public IFluentContractInvoice DeleteContractInvoice(ContractInvoice deleteObject) {
            unitOfWork.contractInvoiceRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentContractInvoice;
        }
   	public IFluentContractInvoice DeleteContractInvoices(List<ContractInvoice> deleteObjects)
        {
            unitOfWork.contractInvoiceRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentContractInvoice;
        }
    }
}
