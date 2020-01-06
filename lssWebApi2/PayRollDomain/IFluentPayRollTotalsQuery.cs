using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.PayRollDomain;


public interface IFluentPayRollTotalsQuery
{
        Task<PayRollTotals> MapToEntity(PayRollTotalsView inputObject);
        Task<IList<PayRollTotals>> MapToEntity(IList<PayRollTotalsView> inputObjects);
    
        Task<PayRollTotalsView> MapToView(PayRollTotals inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollTotals> GetEntityById(long payRollTotalsId);
	  Task<PayRollTotals> GetEntityByNumber(long payRollTotalsNumber);
	Task<PayRollTotalsView> GetViewById(long payRollTotalsId);
	Task<PayRollTotalsView> GetViewByNumber(long payRollTotalsNumber);
}
