

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.AccountReceivableInterestDomain;

namespace lssWebApi2.AccountReceivableInterestDomain
{ 

public interface IFluentAccountReceivableInterest
    {
        IFluentAccountReceivableInterestQuery Query();
        IFluentAccountReceivableInterest Apply();
        IFluentAccountReceivableInterest AddAccountReceivableInterest(AccountReceivableInterest accountReceivableInterest);
        IFluentAccountReceivableInterest UpdateAccountReceivableInterest(AccountReceivableInterest accountReceivableInterest);
        IFluentAccountReceivableInterest DeleteAccountReceivableInterest(AccountReceivableInterest accountReceivableInterest);
     	IFluentAccountReceivableInterest UpdateAccountReceivableInterests(List<AccountReceivableInterest> newObjects);
        IFluentAccountReceivableInterest AddAccountReceivableInterests(List<AccountReceivableInterest> newObjects);
        IFluentAccountReceivableInterest DeleteAccountReceivableInterests(List<AccountReceivableInterest> deleteObjects);
    }
}
