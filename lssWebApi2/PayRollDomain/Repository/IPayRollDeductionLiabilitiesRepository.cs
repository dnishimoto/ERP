

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollDeductionLiabilitiesRepository
    {
        Task<PayRollDeductionLiabilities> GetEntityById(long _payRollDeductionLiabilitiesId);
		Task<List<PayRollDeductionLiabilities>> GetObjectsQueryable(Expression<Func<PayRollDeductionLiabilities, bool>> predicate, string include);
        Task<PayRollDeductionLiabilities> GetEntityByDeductionLiabiltiesCode(int deductionLiabilitiesCode, string deductionLiabilitiesType);
    }
}
