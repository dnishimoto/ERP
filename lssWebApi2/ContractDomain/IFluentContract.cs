

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.ContractDomain
{ 

public interface IFluentContract
    {
        IFluentContractQuery Query();
        IFluentContract Apply();
        IFluentContract AddContract(Contract contract);
        IFluentContract UpdateContract(Contract contract);
        IFluentContract DeleteContract(Contract contract);
     	IFluentContract UpdateContracts(List<Contract> newObjects);
        IFluentContract AddContracts(List<Contract> newObjects);
        IFluentContract DeleteContracts(List<Contract> deleteObjects);
    }
}
