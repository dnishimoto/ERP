using lssWebApi2.AutoMapper;
using lssWebApi2.PayRollDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPayRollDeductionLiabilitiesQuery
{
        Task<PayRollDeductionLiabilities> MapToEntity(PayRollDeductionLiabilitiesView inputObject);
        Task<List<PayRollDeductionLiabilities>> MapToEntity(List<PayRollDeductionLiabilitiesView> inputObjects);
    
        Task<PayRollDeductionLiabilitiesView> MapToView(PayRollDeductionLiabilities inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollDeductionLiabilities> GetEntityById(long payRollDeductionLiabilitiesId);
	  Task<PayRollDeductionLiabilities> GetEntityByNumber(long payRollDeductionLiabilitiesNumber);
	Task<PayRollDeductionLiabilitiesView> GetViewById(long payRollDeductionLiabilitiesId);
	Task<PayRollDeductionLiabilitiesView> GetViewByNumber(long payRollDeductionLiabilitiesNumber);
    Task<PayRollDeductionLiabilitiesView> GetViewByDeductionLiabilitiesCode(int deductionLiabilitiesCode, string deductionLiabilitiesType);
}
