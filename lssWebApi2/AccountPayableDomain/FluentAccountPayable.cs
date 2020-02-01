using lssWebApi2.AccountPayableDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System;
using lssWebApi2.Enumerations;
using lssWebApi2.GeneralLedgerDomain;
using System.Threading.Tasks;
using lssWebApi2.PurchaseOrderDomain;

namespace lssWebApi2.AccountPayableDomain
{

public class FluentAccountPayable :IFluentAccountPayable
    {
        private UnitOfWork unitOfWork;
        private CreateProcessStatus processStatus;

        public FluentAccountPayable(UnitOfWork paramUnitOfWork) { unitOfWork = paramUnitOfWork; }
        public IFluentAccountPayableQuery Query()
        {
            return new FluentAccountPayableQuery(unitOfWork) as IFluentAccountPayableQuery;
        }

        public IFluentAccountPayable UpdatePayableByLedgerView(GeneralLedgerView ledgerView)
        {

            try
            {
                Task<AccountPayable> acctPayTask = Task.Run(async()=> await unitOfWork.accountPayableRepository.GetEntityByGeneralLedger(ledgerView));
                Task.WaitAll(acctPayTask);
                if (acctPayTask.Result!=null) { UpdateAccountPayable(acctPayTask.Result);
                    processStatus = CreateProcessStatus.Update;
                    return this as IFluentAccountPayable;
                }

                processStatus = CreateProcessStatus.Failed;

                return this as IFluentAccountPayable;

            }
            catch (Exception ex)
            { throw new Exception("UpdatePayableByLedgerView", ex); }
        }
        public IFluentAccountPayable CreateAcctPayByPurchaseOrderView(PurchaseOrderView poView)
        {
            try
            {
                //Check if exists
                Task<AccountPayable> acctPayTask =  unitOfWork.accountPayableRepository.GetEntityByPurchaseOrderView(poView);
                Task<NextNumber> nextNumberDocNumberTask = unitOfWork.nextNumberRepository.GetNextNumber(TypeOfPurchaseOrder.DocNumber.ToString());
                Task.WaitAll(acctPayTask, nextNumberDocNumberTask);
                if (acctPayTask.Result!=null)
                {
                    AccountPayable acctPay = acctPayTask.Result;
                    acctPay.DocNumber = nextNumberDocNumberTask.Result?.NextNumberValue;
                    AddAccountPayable(acctPayTask.Result);
                    processStatus= CreateProcessStatus.Insert;
                    return this as IFluentAccountPayable;
                }
                processStatus = CreateProcessStatus.AlreadyExists;
                return this as IFluentAccountPayable;
            }
            catch (Exception ex) { throw new Exception("CreateAcctPayByPurchaseOrderView", ex); }
        }

        public IFluentAccountPayable Apply() {
			try{
            if (this.processStatus == CreateProcessStatus.Insert || this.processStatus == CreateProcessStatus.Update || this.processStatus == CreateProcessStatus.Delete)
            { unitOfWork.CommitChanges(); }
            return this as IFluentAccountPayable;
		    }
            catch (Exception ex) { throw new Exception("Apply", ex); }
        }
        public IFluentAccountPayable AddAccountPayables(List<AccountPayable> newObjects)
        {
            unitOfWork.accountPayableRepository.AddObjects(newObjects);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountPayable;
        }
        public IFluentAccountPayable UpdateAccountPayables(IList<AccountPayable> newObjects)
        {
            foreach (var item in newObjects)
            {
                unitOfWork.accountPayableRepository.UpdateObject(item);
            }
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountPayable;
        }
        public IFluentAccountPayable AddAccountPayable(AccountPayable newObject) {
            unitOfWork.accountPayableRepository.AddObject(newObject);
            this.processStatus = CreateProcessStatus.Insert;
            return this as IFluentAccountPayable;
        }
        public IFluentAccountPayable UpdateAccountPayable(AccountPayable updateObject) {
            unitOfWork.accountPayableRepository.UpdateObject(updateObject);
            this.processStatus = CreateProcessStatus.Update;
            return this as IFluentAccountPayable;

        }
        public IFluentAccountPayable DeleteAccountPayable(AccountPayable deleteObject) {
            unitOfWork.accountPayableRepository.DeleteObject(deleteObject);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountPayable;
        }
   	public IFluentAccountPayable DeleteAccountPayables(List<AccountPayable> deleteObjects)
        {
            unitOfWork.accountPayableRepository.DeleteObjects(deleteObjects);
            this.processStatus = CreateProcessStatus.Delete;
            return this as IFluentAccountPayable;
        }
    }
}
