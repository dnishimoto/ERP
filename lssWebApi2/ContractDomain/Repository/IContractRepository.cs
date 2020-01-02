

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.ContractDomain
{
public interface IContractRepository
    {
        Task<Contract> GetEntityById(long ? contractId);
	    Task<IList<Contract>> GetContractsByCustomerId(long? customerId);
        IQueryable<Contract> GetQueryableByCustomerId(long? customerId);
    }
}
