

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.ContractItemDomain
{
public interface IContractItemRepository
    {
        Task<ContractItem> GetEntityById(long ? contractItemId);
	    Task<ContractItem> FindEntityByExpression(Expression<Func<ContractItem, bool>> predicate);
		Task<IList<ContractItem>> FindEntitiesByExpression(Expression<Func<ContractItem, bool>> predicate);
		IQueryable<ContractItem> GetEntitiesByExpression(Expression<Func<ContractItem, bool>> predicate);
    }
}
