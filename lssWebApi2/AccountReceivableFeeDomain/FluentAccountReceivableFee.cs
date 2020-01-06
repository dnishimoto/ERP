using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.Enumerations;
using lssWebApi2.AccountsReceivableDomain;
using lssWebApi2.AbstractFactory;
using System.Threading.Tasks;

namespace lssWebApi2.AccountReceivableDomain
{

public class FluentAccountReceivableFee :IFluentAccountReceivableFee
    {
 private UnitOfWork unitOfWork = new UnitOfWork();
        private CreateProcessStatus processStatus;

        public FluentAccountReceivableFee() { }
        private ApplicationViewFactory applicationViewFactory = new ApplicationViewFactory();
        public IFluentAccountReceivableFeeQuery Query()
        {
            return new FluentAccountReceivableFeeQuery(unitOfWork) as IFluentAccountReceivableFeeQuery;
        }
        public IFluentAccountReceivableFee CreateLateFee(AccountReceivableFlatView view)
        {
            try
            {
                AccountReceivableFee acctRecFee = new AccountReceivableFee();

                applicationViewFactory.MapAcctRecFeeEntity(ref acctRecFee, view);

                Task<Udc> feeUDCTask = Task.Run(async()=>await unitOfWork.udcRepository.GetUdc("FEES", "TWO_WEEK_LATE"));
                Task.WaitAll(feeUDCTask);


                acctRecFee.FeeAmount = Decimal.Parse(feeUDCTask.Result.Value);

                AddAccountReceivableFee(acctRecFee);

                return this as IFluentAccountReceivableFee;

            }
            catch (Exception ex)
            { throw new Exception("CreateLateFee", ex); }
        }
        public IFluentAccountReceivableFee Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAccountReceivableFee;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentAccountReceivableFee AddAccountReceivableFees(List<AccountReceivableFee> newObjects)
        {
            unitOfWork.accountReceivableFeeRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivableFee;
        }
        public IFluentAccountReceivableFee UpdateAccountReceivableFees(IList<AccountReceivableFee> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.accountReceivableFeeRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivableFee;
        }
        public IFluentAccountReceivableFee AddAccountReceivableFee(AccountReceivableFee newObject) {
            unitOfWork.accountReceivableFeeRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountReceivableFee;
        }
        public IFluentAccountReceivableFee UpdateAccountReceivableFee(AccountReceivableFee updateObject) {
            unitOfWork.accountReceivableFeeRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountReceivableFee;

        }
        public IFluentAccountReceivableFee DeleteAccountReceivableFee(AccountReceivableFee deleteObject) {
            unitOfWork.accountReceivableFeeRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivableFee;
        }
   	public IFluentAccountReceivableFee DeleteAccountReceivableFees(List<AccountReceivableFee> deleteObjects)
        {
            unitOfWork.accountReceivableFeeRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountReceivableFee;
        }
    }
}
