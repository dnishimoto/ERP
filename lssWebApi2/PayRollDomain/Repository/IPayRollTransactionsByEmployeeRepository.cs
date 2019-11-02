

using lssWebApi2.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollTransactionsByEmployeeRepository
    {
        Task<PayRollTransactionsByEmployee> GetEntityById(long _payRollTransactionsByEmployeeId);
        Task<List<PayRollTransactionsByEmployee>> GetObjectsQueryable(Expression<Func<PayRollTransactionsByEmployee, bool>> predicate, string include);
    }
}
