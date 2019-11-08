

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollEarningsRepository
    {
        Task<PayRollEarnings> GetEntityById(long _payRollEarningsId);
		Task<List<PayRollEarnings>> GetObjectsQueryable(Expression<Func<PayRollEarnings, bool>> predicate, string include);
        Task<PayRollEarnings> GetEntityByEarningCode(int earningCode,string earningType);
    }
}
