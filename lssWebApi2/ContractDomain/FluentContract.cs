using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ContractDomain
{

public class FluentContract :IFluentContract
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentContract() { }
        public IFluentContractQuery Query()
        {
            return new FluentContractQuery(unitOfWork) as IFluentContractQuery;
        }
        public IFluentContract Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentContract;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentContract AddContracts(List<Contract> newObjects)
        {
            unitOfWork.contractRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentContract;
        }
        public IFluentContract UpdateContracts(IList<Contract> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.contractRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentContract;
        }
        public IFluentContract AddContract(Contract newObject) {
            unitOfWork.contractRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentContract;
        }
        public IFluentContract UpdateContract(Contract updateObject) {
            unitOfWork.contractRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentContract;

        }
        public IFluentContract DeleteContract(Contract deleteObject) {
            unitOfWork.contractRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentContract;
        }
   	public IFluentContract DeleteContracts(List<Contract> deleteObjects)
        {
            unitOfWork.contractRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentContract;
        }
    }
}
