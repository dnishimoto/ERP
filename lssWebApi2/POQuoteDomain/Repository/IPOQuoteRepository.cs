

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.POQuoteDomain
{
public interface IPOQuoteRepository
    {
        Task<Poquote> GetEntityById(long ? poQuoteId);
		Task<Poquote> FindEntityByExpression(Expression<Func<Poquote, bool>> predicate);
		Task<IList<Poquote>> FindEntitiesByExpression(Expression<Func<Poquote, bool>> predicate);
    }
}
