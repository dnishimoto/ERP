

using lssWebApi2.GeneralLedgerDomain;
using lssWebApi2.PurchaseOrderDomain;
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.AccountPayableDomain
{

    public interface IFluentAccountPayable
    {
        IFluentAccountPayableQuery Query();
        IFluentAccountPayable Apply();
        IFluentAccountPayable AddAccountPayable(AccountPayable accountPayable);
        IFluentAccountPayable UpdateAccountPayable(AccountPayable accountPayable);
        IFluentAccountPayable DeleteAccountPayable(AccountPayable accountPayable);
        IFluentAccountPayable UpdateAccountPayables(List<AccountPayable> newObjects);
        IFluentAccountPayable AddAccountPayables(List<AccountPayable> newObjects);
        IFluentAccountPayable DeleteAccountPayables(List<AccountPayable> deleteObjects);
        IFluentAccountPayable UpdatePayableByLedgerView(GeneralLedgerView ledgerView);
        IFluentAccountPayable CreateAcctPayByPurchaseOrderView(PurchaseOrderView poView);

    }
}