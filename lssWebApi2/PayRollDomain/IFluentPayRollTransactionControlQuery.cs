using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.PayRollDomain;


public interface IFluentPayRollTransactionControlQuery
{
        Task<PayRollTransactionControl> MapToEntity(PayRollTransactionControlView inputObject);
        Task<IList<PayRollTransactionControl>> MapToEntity(IList<PayRollTransactionControlView> inputObjects);
    
        Task<PayRollTransactionControlView> MapToView(PayRollTransactionControl inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollTransactionControl> GetEntityById(long payRollTransactionControlId);
	  Task<PayRollTransactionControl> GetEntityByNumber(long payRollTransactionControlNumber);
	Task<PayRollTransactionControlView> GetViewById(long payRollTransactionControlId);
	Task<PayRollTransactionControlView> GetViewByNumber(long payRollTransactionControlNumber);
}
