using lssWebApi2.AutoMapper;
using lssWebApi2.ContractDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentContractQuery
{
    Task<Contract> MapToEntity(ContractView inputObject);
    Task<IList<Contract>> MapToEntity(IList<ContractView> inputObjects);
    Task<ContractView> MapToView(Contract inputObject);
    Task<NextNumber> GetNextNumber();
    Task<Contract> GetEntityById(long ? contractId);
    Task<Contract> GetEntityByNumber(long contractNumber);
    Task<ContractView> GetViewById(long ? contractId);
    Task<ContractView> GetViewByNumber(long contractNumber);
    Task<IList<ContractView>> GetContractsByCustomerId(long? customerId, long? contractId);
}
