

using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.PayRollDomain
{ 

public interface IFluentPayRollDeductionLiabilities
    {
        IFluentPayRollDeductionLiabilitiesQuery Query();
        IFluentPayRollDeductionLiabilities Apply();
        IFluentPayRollDeductionLiabilities AddPayRollDeductionLiabilities(PayRollDeductionLiabilities payRollDeductionLiabilities);
        IFluentPayRollDeductionLiabilities UpdatePayRollDeductionLiabilities(PayRollDeductionLiabilities payRollDeductionLiabilities);
        IFluentPayRollDeductionLiabilities DeletePayRollDeductionLiabilities(PayRollDeductionLiabilities payRollDeductionLiabilities);
     	IFluentPayRollDeductionLiabilities UpdatePayRollDeductionLiabilitiess(List<PayRollDeductionLiabilities> newObjects);
        IFluentPayRollDeductionLiabilities AddPayRollDeductionLiabilitiess(List<PayRollDeductionLiabilities> newObjects);
        IFluentPayRollDeductionLiabilities DeletePayRollDeductionLiabilitiess(List<PayRollDeductionLiabilities> deleteObjects);
    }
}
