

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.AccountReceivableDetailDomain;

namespace lssWebApi2.AccountReceivableDetailDomain
{ 

public interface IFluentAccountReceivableDetail
    {
        IFluentAccountReceivableDetailQuery Query();
        IFluentAccountReceivableDetail Apply();
        IFluentAccountReceivableDetail AddAccountReceivableDetail(AccountReceivableDetail accountReceivableDetail);
        IFluentAccountReceivableDetail UpdateAccountReceivableDetail(AccountReceivableDetail accountReceivableDetail);
        IFluentAccountReceivableDetail DeleteAccountReceivableDetail(AccountReceivableDetail accountReceivableDetail);
     	IFluentAccountReceivableDetail UpdateAccountReceivableDetails(List<AccountReceivableDetail> newObjects);
        IFluentAccountReceivableDetail AddAccountReceivableDetails(List<AccountReceivableDetail> newObjects);
        IFluentAccountReceivableDetail DeleteAccountReceivableDetails(List<AccountReceivableDetail> deleteObjects);
    }
}
