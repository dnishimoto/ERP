using lssWebApi2.AutoMapper;
using lssWebApi2.CustomerClaimDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentCustomerClaimQuery
{
    Task<CustomerClaim> MapToEntity(CustomerClaimView inputObject);
    Task<List<CustomerClaim>> MapToEntity(List<CustomerClaimView> inputObjects);
    Task<CustomerClaimView> MapToView(CustomerClaim inputObject);
    Task<NextNumber> GetNextNumber();
    Task<CustomerClaim> GetEntityById(long ? customerClaimId);
    Task<CustomerClaim> GetEntityByNumber(long customerClaimNumber);
    Task<CustomerClaimView> GetViewById(long ? customerClaimId);
    Task<CustomerClaimView> GetViewByNumber(long customerClaimNumber);
    Task<IList<CustomerClaimView>> GetCustomerClaimsByCustomerId(long customerId);
}
