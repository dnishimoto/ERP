using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollTransactionControl :IFluentPayRollTransactionControl
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPayRollTransactionControl() { }
        public IFluentPayRollTransactionControlQuery Query()
        {
            return new FluentPayRollTransactionControlQuery(unitOfWork) as IFluentPayRollTransactionControlQuery;
        }
        public IFluentPayRollTransactionControl Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollTransactionControl;
        }
        public IFluentPayRollTransactionControl AddPayRollTransactionControls(List<PayRollTransactionControl> newObjects)
        {
            unitOfWork.payRollTransactionControlRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTransactionControl;
        }
        public IFluentPayRollTransactionControl UpdatePayRollTransactionControls(List<PayRollTransactionControl> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollTransactionControlRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTransactionControl;
        }
        public IFluentPayRollTransactionControl AddPayRollTransactionControl(PayRollTransactionControl newObject) {
            unitOfWork.payRollTransactionControlRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTransactionControl;
        }
        public IFluentPayRollTransactionControl UpdatePayRollTransactionControl(PayRollTransactionControl updateObject) {
            unitOfWork.payRollTransactionControlRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTransactionControl;

        }
        public IFluentPayRollTransactionControl DeletePayRollTransactionControl(PayRollTransactionControl deleteObject) {
            unitOfWork.payRollTransactionControlRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTransactionControl;
        }
   	public IFluentPayRollTransactionControl DeletePayRollTransactionControls(List<PayRollTransactionControl> deleteObjects)
        {
            unitOfWork.payRollTransactionControlRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTransactionControl;
        }
    }
}
