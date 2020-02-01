

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.AccountReceivableDomain;

namespace lssWebApi2.AccountReceivableDomain
{ 

public interface IFluentAccountReceivableFee
    {
        IFluentAccountReceivableFeeQuery Query();
        IFluentAccountReceivableFee Apply();
        IFluentAccountReceivableFee AddAccountReceivableFee(AccountReceivableFee accountReceivableFee);
        IFluentAccountReceivableFee UpdateAccountReceivableFee(AccountReceivableFee accountReceivableFee);
        IFluentAccountReceivableFee DeleteAccountReceivableFee(AccountReceivableFee accountReceivableFee);
     	IFluentAccountReceivableFee UpdateAccountReceivableFees(IList<AccountReceivableFee> newObjects);
        IFluentAccountReceivableFee AddAccountReceivableFees(List<AccountReceivableFee> newObjects);
        IFluentAccountReceivableFee DeleteAccountReceivableFees(List<AccountReceivableFee> deleteObjects);
        IFluentAccountReceivableFee CreateLateFee(AccountReceivableFlatView view);
    }
}
