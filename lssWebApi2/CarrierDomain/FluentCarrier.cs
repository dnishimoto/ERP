using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.CarrierDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.CarrierDomain
{

public class FluentCarrier :IFluentCarrier
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentCarrier() { }
        public IFluentCarrierQuery Query()
        {
            return new FluentCarrierQuery(unitOfWork) as IFluentCarrierQuery;
        }
        public IFluentCarrier Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentCarrier;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentCarrier AddCarriers(List<Carrier> newObjects)
        {
            unitOfWork.carrierRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCarrier;
        }
        public IFluentCarrier UpdateCarriers(List<Carrier> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.carrierRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCarrier;
        }
        public IFluentCarrier AddCarrier(Carrier newObject) {
            unitOfWork.carrierRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCarrier;
        }
        public IFluentCarrier UpdateCarrier(Carrier updateObject) {
            unitOfWork.carrierRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCarrier;

        }
        public IFluentCarrier DeleteCarrier(Carrier deleteObject) {
            unitOfWork.carrierRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCarrier;
        }
   	public IFluentCarrier DeleteCarriers(List<Carrier> deleteObjects)
        {
            unitOfWork.carrierRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCarrier;
        }
    }
}
