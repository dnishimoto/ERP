

using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public interface IPayRollTransactionsByEmployeeRepository
    {
        Task<PayRollTransactionsByEmployee> GetEntityById(long _payRollTransactionsByEmployeeId);
        IQueryable<PayRollTransactionsByEmployee> GetEntitiesByExpression(Expression<Func<PayRollTransactionsByEmployee, bool>> predicate);
    }
}
