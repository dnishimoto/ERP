using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollPaySequence :IFluentPayRollPaySequence
    {
 private UnitOfWork unitOfWork ;
        private CreateProcessStatus processStatus;

        public FluentPayRollPaySequence(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentPayRollPaySequenceQuery Query()
        {
            return new FluentPayRollPaySequenceQuery(unitOfWork) as IFluentPayRollPaySequenceQuery;
        }
        public IFluentPayRollPaySequence Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollPaySequence;
        }
        public IFluentPayRollPaySequence AddPayRollPaySequences(List<PayRollPaySequence> newObjects)
        {
            unitOfWork.payRollPaySequenceRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollPaySequence;
        }
        public IFluentPayRollPaySequence UpdatePayRollPaySequences(IList<PayRollPaySequence> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollPaySequenceRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollPaySequence;
        }
        public IFluentPayRollPaySequence AddPayRollPaySequence(PayRollPaySequence newObject) {
            unitOfWork.payRollPaySequenceRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollPaySequence;
        }
        public IFluentPayRollPaySequence UpdatePayRollPaySequence(PayRollPaySequence updateObject) {
            unitOfWork.payRollPaySequenceRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollPaySequence;

        }
        public IFluentPayRollPaySequence DeletePayRollPaySequence(PayRollPaySequence deleteObject) {
            unitOfWork.payRollPaySequenceRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollPaySequence;
        }
   	public IFluentPayRollPaySequence DeletePayRollPaySequences(List<PayRollPaySequence> deleteObjects)
        {
            unitOfWork.payRollPaySequenceRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollPaySequence;
        }
    }
}
