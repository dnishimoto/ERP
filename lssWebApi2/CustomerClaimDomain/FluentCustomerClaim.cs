using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;


namespace lssWebApi2.CustomerClaimDomain
{

public class FluentCustomerClaim :IFluentCustomerClaim
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentCustomerClaim() { }
        public IFluentCustomerClaimQuery Query()
        {
            return new FluentCustomerClaimQuery(unitOfWork) as IFluentCustomerClaimQuery;
        }
        public IFluentCustomerClaim Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentCustomerClaim;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentCustomerClaim AddCustomerClaims(List<CustomerClaim> newObjects)
        {
            unitOfWork.customerClaimRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCustomerClaim;
        }
        public IFluentCustomerClaim UpdateCustomerClaims(IList<CustomerClaim> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.customerClaimRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCustomerClaim;
        }
        public IFluentCustomerClaim AddCustomerClaim(CustomerClaim newObject) {
            unitOfWork.customerClaimRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentCustomerClaim;
        }
        public IFluentCustomerClaim UpdateCustomerClaim(CustomerClaim updateObject) {
            unitOfWork.customerClaimRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentCustomerClaim;

        }
        public IFluentCustomerClaim DeleteCustomerClaim(CustomerClaim deleteObject) {
            unitOfWork.customerClaimRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCustomerClaim;
        }
   	public IFluentCustomerClaim DeleteCustomerClaims(List<CustomerClaim> deleteObjects)
        {
            unitOfWork.customerClaimRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentCustomerClaim;
        }
    }
}
