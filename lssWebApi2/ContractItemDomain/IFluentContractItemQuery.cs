using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ContractItemDomain
{
  public interface IFluentContractItemQuery
  {
     Task<ContractItem> MapToEntity(ContractItemView inputObject);
     Task<IList<ContractItem>> MapToEntity(IList<ContractItemView> inputObjects);
     Task<ContractItemView> MapToView(ContractItem inputObject);
     Task<NextNumber> GetNextNumber();
	 Task<ContractItem> GetEntityById(long ? contractItemId);
	 Task<ContractItem> GetEntityByNumber(long contractItemNumber);
	 Task<ContractItemView> GetViewById(long ? contractItemId);
	 Task<ContractItemView> GetViewByNumber(long contractItemNumber);
  }
}
