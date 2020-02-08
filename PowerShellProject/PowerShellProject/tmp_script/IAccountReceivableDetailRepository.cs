

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace lssWebApi2.AccountReceivableDetailDomain
{
public interface IAccountReceivableDetailRepository
    {
        Task<AccountReceivableDetail> GetEntityById(long ? accountReceivableDetailId);
	    Task<AccountReceivableDetail> FindEntityByExpression(Expression<Func<AccountReceivableDetail, bool>> predicate);
		Task<IList<AccountReceivableDetail>> FindEntitiesByExpression(Expression<Func<AccountReceivableDetail, bool>> predicate);
		IQueryable<AccountReceivableDetail> GetEntitiesByExpression(Expression<Func<AccountReceivableDetail, bool>> predicate);
    }
}
