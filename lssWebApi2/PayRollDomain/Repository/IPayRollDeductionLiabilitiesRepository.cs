

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace lssWebApi2.PayRollDomain
{
public interface IPayRollDeductionLiabilitiesRepository
    {
        Task<PayRollDeductionLiabilities> GetEntityById(long _payRollDeductionLiabilitiesId);
        Task<PayRollDeductionLiabilities> GetEntityByDeductionLiabiltiesCode(int deductionLiabilitiesCode, string deductionLiabilitiesType);
    }
}
