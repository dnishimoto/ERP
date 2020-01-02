

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.AccountReceivableInterestDomain
{
public interface IAccountReceivableInterestRepository
    {
        Task<AccountReceivableInterest> GetEntityById(long ? accountReceivableInterestId);
		Task<AccountReceivableInterest> FindEntityByExpression(Expression<Func<AccountReceivableInterest, bool>> predicate);
		Task<IList<AccountReceivableInterest>> FindEntitiesByExpression(Expression<Func<AccountReceivableInterest, bool>> predicate);
    }
}
