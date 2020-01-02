using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ServiceInformationInvoiceDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ServiceInformationInvoiceDomain
{

public class FluentServiceInformationInvoice :IFluentServiceInformationInvoice
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentServiceInformationInvoice() { }
        public IFluentServiceInformationInvoiceQuery Query()
        {
            return new FluentServiceInformationInvoiceQuery(unitOfWork) as IFluentServiceInformationInvoiceQuery;
        }
        public IFluentServiceInformationInvoice Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentServiceInformationInvoice;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentServiceInformationInvoice AddServiceInformationInvoices(List<ServiceInformationInvoice> newObjects)
        {
            unitOfWork.serviceInformationInvoiceRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentServiceInformationInvoice;
        }
        public IFluentServiceInformationInvoice UpdateServiceInformationInvoices(List<ServiceInformationInvoice> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.serviceInformationInvoiceRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentServiceInformationInvoice;
        }
        public IFluentServiceInformationInvoice AddServiceInformationInvoice(ServiceInformationInvoice newObject) {
            unitOfWork.serviceInformationInvoiceRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentServiceInformationInvoice;
        }
        public IFluentServiceInformationInvoice UpdateServiceInformationInvoice(ServiceInformationInvoice updateObject) {
            unitOfWork.serviceInformationInvoiceRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentServiceInformationInvoice;

        }
        public IFluentServiceInformationInvoice DeleteServiceInformationInvoice(ServiceInformationInvoice deleteObject) {
            unitOfWork.serviceInformationInvoiceRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentServiceInformationInvoice;
        }
   	public IFluentServiceInformationInvoice DeleteServiceInformationInvoices(List<ServiceInformationInvoice> deleteObjects)
        {
            unitOfWork.serviceInformationInvoiceRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentServiceInformationInvoice;
        }
    }
}
