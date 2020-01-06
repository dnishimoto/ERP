using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollGroup :IFluentPayRollGroup
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPayRollGroup() { }
        public IFluentPayRollGroupQuery Query()
        {
            return new FluentPayRollGroupQuery(unitOfWork) as IFluentPayRollGroupQuery;
        }
        public IFluentPayRollGroup Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollGroup;
        }
        public IFluentPayRollGroup AddPayRollGroups(List<PayRollGroup> newObjects)
        {
            unitOfWork.payRollGroupRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollGroup;
        }
        public IFluentPayRollGroup UpdatePayRollGroups(IList<PayRollGroup> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollGroupRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollGroup;
        }
        public IFluentPayRollGroup AddPayRollGroup(PayRollGroup newObject) {
            unitOfWork.payRollGroupRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollGroup;
        }
        public IFluentPayRollGroup UpdatePayRollGroup(PayRollGroup updateObject) {
            unitOfWork.payRollGroupRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollGroup;

        }
        public IFluentPayRollGroup DeletePayRollGroup(PayRollGroup deleteObject) {
            unitOfWork.payRollGroupRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollGroup;
        }
   	public IFluentPayRollGroup DeletePayRollGroups(List<PayRollGroup> deleteObjects)
        {
            unitOfWork.payRollGroupRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollGroup;
        }
    }
}
