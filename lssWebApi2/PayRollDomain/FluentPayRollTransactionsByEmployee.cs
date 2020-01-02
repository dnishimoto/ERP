using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;

namespace lssWebApi2.PayRollDomain
{

public class FluentPayRollTransactionsByEmployee :IFluentPayRollTransactionsByEmployee
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentPayRollTransactionsByEmployee() { }
        public IFluentPayRollTransactionsByEmployeeQuery Query()
        {
            return new FluentPayRollTransactionsByEmployeeQuery(unitOfWork) as IFluentPayRollTransactionsByEmployeeQuery;
        }
        public IFluentPayRollTransactionsByEmployee Apply() {
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentPayRollTransactionsByEmployee;
        }
        public IFluentPayRollTransactionsByEmployee AddPayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> newObjects)
        {
            unitOfWork.payRollTransactionsByEmployeeRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTransactionsByEmployee;
        }
        public IFluentPayRollTransactionsByEmployee UpdatePayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.payRollTransactionsByEmployeeRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTransactionsByEmployee;
        }
        public IFluentPayRollTransactionsByEmployee AddPayRollTransactionsByEmployee(PayRollTransactionsByEmployee newObject) {
            unitOfWork.payRollTransactionsByEmployeeRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentPayRollTransactionsByEmployee;
        }
        public IFluentPayRollTransactionsByEmployee UpdatePayRollTransactionsByEmployee(PayRollTransactionsByEmployee updateObject) {
            unitOfWork.payRollTransactionsByEmployeeRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentPayRollTransactionsByEmployee;

        }
        public IFluentPayRollTransactionsByEmployee DeletePayRollTransactionsByEmployee(PayRollTransactionsByEmployee deleteObject) {
            unitOfWork.payRollTransactionsByEmployeeRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTransactionsByEmployee;
        }
   	public IFluentPayRollTransactionsByEmployee DeletePayRollTransactionsByEmployees(List<PayRollTransactionsByEmployee> deleteObjects)
        {
            unitOfWork.payRollTransactionsByEmployeeRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentPayRollTransactionsByEmployee;
        }
    }
}
