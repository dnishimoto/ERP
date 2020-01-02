

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.PayRollDomain
{
public interface IPayRollEarningsRepository
    {
        Task<PayRollEarnings> GetEntityById(long _payRollEarningsId);
	    Task<PayRollEarnings> GetEntityByEarningCode(int earningCode,string earningType);
    }
}
