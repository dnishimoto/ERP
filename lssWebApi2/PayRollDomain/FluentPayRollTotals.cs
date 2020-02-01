using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollTotals :IFluentPayRollTotals
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentPayRollTotals(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPayRollTotalsQuery Query()
        {
            return new FluentPayRollTotalsQuery(unitOfWork) as IFluentPayRollTotalsQuery;
        }
        public IFluentPayRollTotals Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollTotals;
        }
        public IFluentPayRollTotals AddPayRollTotalss(List<PayRollTotals> newObjects)
        {
            unitOfWork.payRollTotalsRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTotals;
        }
        public IFluentPayRollTotals UpdatePayRollTotalss(IList<PayRollTotals> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollTotalsRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTotals;
        }
        public IFluentPayRollTotals AddPayRollTotals(PayRollTotals newObject) {
            unitOfWork.payRollTotalsRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTotals;
        }
        public IFluentPayRollTotals UpdatePayRollTotals(PayRollTotals updateObject) {
            unitOfWork.payRollTotalsRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTotals;

        }
        public IFluentPayRollTotals DeletePayRollTotals(PayRollTotals deleteObject) {
            unitOfWork.payRollTotalsRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTotals;
        }
   	public IFluentPayRollTotals DeletePayRollTotalss(List<PayRollTotals> deleteObjects)
        {
            unitOfWork.payRollTotalsRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTotals;
        }
    }
}
