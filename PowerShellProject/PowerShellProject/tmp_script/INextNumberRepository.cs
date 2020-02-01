

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.NextNumberDomain
{
public interface INextNumberRepository
    {
        Task<NextNumber> GetEntityById(long ? nextNumberId);
	    Task<NextNumber> FindEntityByExpression(Expression<Func<NextNumber, bool>> predicate);
		Task<IList<NextNumber>> FindEntitiesByExpression(Expression<Func<NextNumber, bool>> predicate);
		IQueryable<NextNumber> GetEntitiesByExpression(Expression<Func<NextNumber, bool>> predicate);
    }
}
