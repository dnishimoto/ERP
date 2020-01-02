using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.ContractItemDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.ContractItemDomain
{

public class FluentContractItem :IFluentContractItem
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentContractItem() { }
        public IFluentContractItemQuery Query()
        {
            return new FluentContractItemQuery(unitOfWork) as IFluentContractItemQuery;
        }
        public IFluentContractItem Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentContractItem;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentContractItem AddContractItems(List<ContractItem> newObjects)
        {
            unitOfWork.contractItemRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentContractItem;
        }
        public IFluentContractItem UpdateContractItems(List<ContractItem> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.contractItemRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentContractItem;
        }
        public IFluentContractItem AddContractItem(ContractItem newObject) {
            unitOfWork.contractItemRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentContractItem;
        }
        public IFluentContractItem UpdateContractItem(ContractItem updateObject) {
            unitOfWork.contractItemRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentContractItem;

        }
        public IFluentContractItem DeleteContractItem(ContractItem deleteObject) {
            unitOfWork.contractItemRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentContractItem;
        }
   	public IFluentContractItem DeleteContractItems(List<ContractItem> deleteObjects)
        {
            unitOfWork.contractItemRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentContractItem;
        }
    }
}
