using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using lssWebApi2.Enumerations;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollLedger :IFluentPayRollLedger
    {
 private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentPayRollLedger(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPayRollLedgerQuery Query()
        {
            return new FluentPayRollLedgerQuery(unitOfWork) as IFluentPayRollLedgerQuery;
        }
        public IFluentPayRollLedger Apply() {
            try
            {
                if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
                { unitOfWork.CommitChanges(); }
                return this as IFluentPayRollLedger;
            }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentPayRollLedger AddPayRollLedgers(List<PayRollLedger> newObjects)
        {
            unitOfWork.payRollLedgerRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollLedger;
        }
        public IFluentPayRollLedger UpdatePayRollLedgers(IList<PayRollLedger> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollLedgerRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollLedger;
        }
        public IFluentPayRollLedger AddPayRollLedger(PayRollLedger newObject) {
            unitOfWork.payRollLedgerRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollLedger;
        }
        public IFluentPayRollLedger UpdatePayRollLedger(PayRollLedger updateObject) {
            unitOfWork.payRollLedgerRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollLedger;

        }
        public IFluentPayRollLedger DeletePayRollLedger(PayRollLedger deleteObject) {
            unitOfWork.payRollLedgerRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollLedger;
        }
   	public IFluentPayRollLedger DeletePayRollLedgers(List<PayRollLedger> deleteObjects)
        {
            unitOfWork.payRollLedgerRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollLedger;
        }
    }
}
