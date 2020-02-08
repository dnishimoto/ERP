

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.AccountPayableDetailDomain
{
public interface IAccountPayableDetailRepository
    {
        Task<AccountPayableDetail> GetEntityById(long ? accountPyableDetailId);
	    Task<AccountPayableDetail> FindEntityByExpression(Expression<Func<AccountPayableDetail, bool>> predicate);
		Task<IList<AccountPayableDetail>> FindEntitiesByExpression(Expression<Func<AccountPayableDetail, bool>> predicate);
		IQueryable<AccountPayableDetail> GetEntitiesByExpression(Expression<Func<AccountPayableDetail, bool>> predicate);
    }
}
