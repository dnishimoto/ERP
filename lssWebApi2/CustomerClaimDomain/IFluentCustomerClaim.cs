

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.CustomerClaimDomain
{ 

public interface IFluentCustomerClaim
    {
        IFluentCustomerClaimQuery Query();
        IFluentCustomerClaim Apply();
        IFluentCustomerClaim AddCustomerClaim(CustomerClaim customerClaim);
        IFluentCustomerClaim UpdateCustomerClaim(CustomerClaim customerClaim);
        IFluentCustomerClaim DeleteCustomerClaim(CustomerClaim customerClaim);
     	IFluentCustomerClaim UpdateCustomerClaims(IList<CustomerClaim> newObjects);
        IFluentCustomerClaim AddCustomerClaims(List<CustomerClaim> newObjects);
        IFluentCustomerClaim DeleteCustomerClaims(List<CustomerClaim> deleteObjects);
    }
}
