

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.AccountPayableDetailDomain
{ 

public interface IFluentAccountPayableDetail
    {
        IFluentAccountPayableDetailQuery Query();
        IFluentAccountPayableDetail Apply();
        IFluentAccountPayableDetail AddAccountPayableDetail(AccountPayableDetail accountPyableDetail);
        IFluentAccountPayableDetail UpdateAccountPayableDetail(AccountPayableDetail accountPyableDetail);
        IFluentAccountPayableDetail DeleteAccountPayableDetail(AccountPayableDetail accountPyableDetail);
     	IFluentAccountPayableDetail UpdateAccountPayableDetails(List<AccountPayableDetail> newObjects);
        IFluentAccountPayableDetail AddAccountPayableDetails(List<AccountPayableDetail> newObjects);
        IFluentAccountPayableDetail DeleteAccountPayableDetails(List<AccountPayableDetail> deleteObjects);
    }
}
