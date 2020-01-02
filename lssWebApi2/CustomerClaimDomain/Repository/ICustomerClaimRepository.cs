

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.CustomerClaimDomain
{
public interface ICustomerClaimRepository
    {
        Task<CustomerClaim> GetEntityById(long ? customerClaimId);
	    Task<IList<CustomerClaim>> GetEntitiesByCustomerId(long customerId);
    }
}
