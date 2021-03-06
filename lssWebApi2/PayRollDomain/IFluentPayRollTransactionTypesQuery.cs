using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.PayRollDomain;


public interface IFluentPayRollTransactionTypesQuery
{
        Task<PayRollTransactionTypes> MapToEntity(PayRollTransactionTypesView inputObject);
        Task<IList<PayRollTransactionTypes>> MapToEntity(IList<PayRollTransactionTypesView> inputObjects);
    
        Task<PayRollTransactionTypesView> MapToView(PayRollTransactionTypes inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollTransactionTypes> GetEntityById(long payRollTransactionTypesId);
	  Task<PayRollTransactionTypes> GetEntityByNumber(long payRollTransactionTypesNumber);
	Task<PayRollTransactionTypesView> GetViewById(long payRollTransactionTypesId);
	Task<PayRollTransactionTypesView> GetViewByNumber(long payRollTransactionTypesNumber);
}
