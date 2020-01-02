using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollCurrentPaySequence :IFluentPayRollCurrentPaySequence
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPayRollCurrentPaySequence() { }
        public IFluentPayRollCurrentPaySequenceQuery Query()
        {
            return new FluentPayRollCurrentPaySequenceQuery(unitOfWork) as IFluentPayRollCurrentPaySequenceQuery;
        }
        public IFluentPayRollCurrentPaySequence Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollCurrentPaySequence;
        }
        public IFluentPayRollCurrentPaySequence AddPayRollCurrentPaySequences(List<PayRollCurrentPaySequence> newObjects)
        {
            unitOfWork.payRollCurrentPaySequenceRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollCurrentPaySequence;
        }
        public IFluentPayRollCurrentPaySequence UpdatePayRollCurrentPaySequences(List<PayRollCurrentPaySequence> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollCurrentPaySequenceRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollCurrentPaySequence;
        }
        public IFluentPayRollCurrentPaySequence AddPayRollCurrentPaySequence(PayRollCurrentPaySequence newObject) {
            unitOfWork.payRollCurrentPaySequenceRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollCurrentPaySequence;
        }
        public IFluentPayRollCurrentPaySequence UpdatePayRollCurrentPaySequence(PayRollCurrentPaySequence updateObject) {
            unitOfWork.payRollCurrentPaySequenceRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollCurrentPaySequence;

        }
        public IFluentPayRollCurrentPaySequence DeletePayRollCurrentPaySequence(PayRollCurrentPaySequence deleteObject) {
            unitOfWork.payRollCurrentPaySequenceRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollCurrentPaySequence;
        }
   	public IFluentPayRollCurrentPaySequence DeletePayRollCurrentPaySequences(List<PayRollCurrentPaySequence> deleteObjects)
        {
            unitOfWork.payRollCurrentPaySequenceRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollCurrentPaySequence;
        }
    }
}
