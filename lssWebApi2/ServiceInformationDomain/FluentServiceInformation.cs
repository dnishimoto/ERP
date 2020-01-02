using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ServiceInformationDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ServiceInformationDomain
{

public class FluentServiceInformation :IFluentServiceInformation
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentServiceInformation() { }
        public IFluentServiceInformationQuery Query()
        {
            return new FluentServiceInformationQuery(unitOfWork) as IFluentServiceInformationQuery;
        }
        public IFluentServiceInformation Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentServiceInformation;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentServiceInformation AddServiceInformations(List<ServiceInformation> newObjects)
        {
            unitOfWork.serviceInformationRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentServiceInformation;
        }
        public IFluentServiceInformation UpdateServiceInformations(List<ServiceInformation> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.serviceInformationRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentServiceInformation;
        }
        public IFluentServiceInformation AddServiceInformation(ServiceInformation newObject) {
            unitOfWork.serviceInformationRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentServiceInformation;
        }
        public IFluentServiceInformation UpdateServiceInformation(ServiceInformation updateObject) {
            unitOfWork.serviceInformationRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentServiceInformation;

        }
        public IFluentServiceInformation DeleteServiceInformation(ServiceInformation deleteObject) {
            unitOfWork.serviceInformationRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentServiceInformation;
        }
   	public IFluentServiceInformation DeleteServiceInformations(List<ServiceInformation> deleteObjects)
        {
            unitOfWork.serviceInformationRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentServiceInformation;
        }
    }
}
