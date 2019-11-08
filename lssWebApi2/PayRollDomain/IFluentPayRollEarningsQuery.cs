using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP_Core2.PayRollDomain;


public interface IFluentPayRollEarningsQuery
{
        Task<PayRollEarnings> MapToEntity(PayRollEarningsView inputObject);
        Task<List<PayRollEarnings>> MapToEntity(List<PayRollEarningsView> inputObjects);
    
        Task<PayRollEarningsView> MapToView(PayRollEarnings inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollEarnings> GetEntityById(long payRollEarningsId);
	  Task<PayRollEarnings> GetEntityByNumber(long payRollEarningsNumber);
	Task<PayRollEarningsView> GetViewById(long payRollEarningsId);
	Task<PayRollEarningsView> GetViewByNumber(long payRollEarningsNumber);
    Task<PayRollEarningsView> GetViewByEarningCode(int earningCode,string earningType);
}
