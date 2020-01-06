using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.BuyerDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.BuyerDomain
{

public class FluentBuyer :IFluentBuyer
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentBuyer() { }
        public IFluentBuyerQuery Query()
        {
            return new FluentBuyerQuery(unitOfWork) as IFluentBuyerQuery;
        }
        public IFluentBuyer Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentBuyer;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentBuyer AddBuyers(List<Buyer> newObjects)
        {
            unitOfWork.buyerRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBuyer;
        }
        public IFluentBuyer UpdateBuyers(IList<Buyer> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.buyerRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBuyer;
        }
        public IFluentBuyer AddBuyer(Buyer newObject) {
            unitOfWork.buyerRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentBuyer;
        }
        public IFluentBuyer UpdateBuyer(Buyer updateObject) {
            unitOfWork.buyerRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentBuyer;

        }
        public IFluentBuyer DeleteBuyer(Buyer deleteObject) {
            unitOfWork.buyerRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBuyer;
        }
   	public IFluentBuyer DeleteBuyers(List<Buyer> deleteObjects)
        {
            unitOfWork.buyerRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentBuyer;
        }
    }
}
