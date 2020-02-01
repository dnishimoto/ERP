using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.NextNumberDomain;
using lssWebApi2.Enumerations;

namespace lssWebApi2.NextNumberDomain
{

public class FluentNextNumber :IFluentNextNumber
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentNextNumber(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentNextNumberQuery Query()
        {
            return new FluentNextNumberQuery(unitOfWork) as IFluentNextNumberQuery;
        }
        public IFluentNextNumber Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentNextNumber;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentNextNumber AddNextNumbers(List<NextNumber> newObjects)
        {
            unitOfWork.nextNumberRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentNextNumber;
        }
        public IFluentNextNumber UpdateNextNumbers(List<NextNumber> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.nextNumberRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentNextNumber;
        }
        public IFluentNextNumber AddNextNumber(NextNumber newObject) {
            unitOfWork.nextNumberRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentNextNumber;
        }
        public IFluentNextNumber UpdateNextNumber(NextNumber updateObject) {
            unitOfWork.nextNumberRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentNextNumber;

        }
        public IFluentNextNumber DeleteNextNumber(NextNumber deleteObject) {
            unitOfWork.nextNumberRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentNextNumber;
        }
   	public IFluentNextNumber DeleteNextNumbers(List<NextNumber> deleteObjects)
        {
            unitOfWork.nextNumberRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentNextNumber;
        }
    }
}
