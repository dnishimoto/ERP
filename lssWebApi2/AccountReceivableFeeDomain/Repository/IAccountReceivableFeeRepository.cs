

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.AccountReceivableDomain
{
public interface IAccountReceivableFeeRepository
    {
        Task<AccountReceivableFee> GetEntityById(long ? accountReceivableFeeId);
	    Task<AccountReceivableFee> FindEntityByExpression(Expression<Func<AccountReceivableFee, bool>> predicate);
		Task<IList<AccountReceivableFee>> FindEntitiesByExpression(Expression<Func<AccountReceivableFee, bool>> predicate);
        Task<Decimal> GetFeeAmountById(long? acctRecId);
    }
}
