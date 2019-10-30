using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP_Core2.PayRollDomain;


public interface IFluentPayRollTransactionControlQuery
{
        Task<PayRollTransactionControl> MapToEntity(PayRollTransactionControlView inputObject);
        Task<List<PayRollTransactionControl>> MapToEntity(List<PayRollTransactionControlView> inputObjects);
    
        Task<PayRollTransactionControlView> MapToView(PayRollTransactionControl inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollTransactionControl> GetEntityById(long payRollTransactionControlId);
	  Task<PayRollTransactionControl> GetEntityByNumber(long payRollTransactionControlNumber);
	Task<PayRollTransactionControlView> GetViewById(long payRollTransactionControlId);
	Task<PayRollTransactionControlView> GetViewByNumber(long payRollTransactionControlNumber);
}
