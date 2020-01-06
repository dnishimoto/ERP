using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.PayRollDomain;


public interface IFluentPayRollTransactionsByEmployeeQuery
{
        Task<PayRollTransactionsByEmployee> MapToEntity(PayRollTransactionsByEmployeeView inputObject);
        Task<IList<PayRollTransactionsByEmployee>> MapToEntity(IList<PayRollTransactionsByEmployeeView> inputObjects);
    
        Task<PayRollTransactionsByEmployeeView> MapToView(PayRollTransactionsByEmployee inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollTransactionsByEmployee> GetEntityById(long payRollTransactionsByEmployeeId);
	  Task<PayRollTransactionsByEmployee> GetEntityByNumber(long payRollTransactionsByEmployeeNumber);
	Task<PayRollTransactionsByEmployeeView> GetViewById(long payRollTransactionsByEmployeeId);
	Task<PayRollTransactionsByEmployeeView> GetViewByNumber(long payRollTransactionsByEmployeeNumber);
    Task<List<PayRollTransactionsByEmployeeView>> GetTransactionsByEmployeeViews(long employee);
    Task<PayRollTransactionsByEmployee> GetEntityByEmployeeAndTransactionCodeAndType(long employee, int payRollTransactionCode, string transactionType);
}
