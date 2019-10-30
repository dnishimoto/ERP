using ERP_Core2.AccountPayableDomain;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.PayRollDomain
{

public class FluentPayRollTransactionTypes :IFluentPayRollTransactionTypes
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPayRollTransactionTypes() { }
        public IFluentPayRollTransactionTypesQuery Query()
        {
            return new FluentPayRollTransactionTypesQuery(unitOfWork) as IFluentPayRollTransactionTypesQuery;
        }
        public IFluentPayRollTransactionTypes Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollTransactionTypes;
        }
        public IFluentPayRollTransactionTypes AddPayRollTransactionTypess(List<PayRollTransactionTypes> newObjects)
        {
            unitOfWork.payRollTransactionTypesRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTransactionTypes;
        }
        public IFluentPayRollTransactionTypes UpdatePayRollTransactionTypess(List<PayRollTransactionTypes> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollTransactionTypesRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTransactionTypes;
        }
        public IFluentPayRollTransactionTypes AddPayRollTransactionTypes(PayRollTransactionTypes newObject) {
            unitOfWork.payRollTransactionTypesRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTransactionTypes;
        }
        public IFluentPayRollTransactionTypes UpdatePayRollTransactionTypes(PayRollTransactionTypes updateObject) {
            unitOfWork.payRollTransactionTypesRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTransactionTypes;

        }
        public IFluentPayRollTransactionTypes DeletePayRollTransactionTypes(PayRollTransactionTypes deleteObject) {
            unitOfWork.payRollTransactionTypesRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTransactionTypes;
        }
   	public IFluentPayRollTransactionTypes DeletePayRollTransactionTypess(List<PayRollTransactionTypes> deleteObjects)
        {
            unitOfWork.payRollTransactionTypesRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTransactionTypes;
        }
    }
}
