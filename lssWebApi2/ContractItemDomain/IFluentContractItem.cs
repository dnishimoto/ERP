

using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using lssWebApi2.ContractItemDomain;

namespace lssWebApi2.ContractItemDomain
{ 

public interface IFluentContractItem
    {
        IFluentContractItemQuery Query();
        IFluentContractItem Apply();
        IFluentContractItem AddContractItem(ContractItem contractItem);
        IFluentContractItem UpdateContractItem(ContractItem contractItem);
        IFluentContractItem DeleteContractItem(ContractItem contractItem);
     	IFluentContractItem UpdateContractItems(IList<ContractItem> newObjects);
        IFluentContractItem AddContractItems(List<ContractItem> newObjects);
        IFluentContractItem DeleteContractItems(List<ContractItem> deleteObjects);
    }
}
