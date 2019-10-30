using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP_Core2.PayRollDomain;


public interface IFluentPayRollTransactionTypesQuery
{
        Task<PayRollTransactionTypes> MapToEntity(PayRollTransactionTypesView inputObject);
        Task<List<PayRollTransactionTypes>> MapToEntity(List<PayRollTransactionTypesView> inputObjects);
    
        Task<PayRollTransactionTypesView> MapToView(PayRollTransactionTypes inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollTransactionTypes> GetEntityById(long payRollTransactionTypesId);
	  Task<PayRollTransactionTypes> GetEntityByNumber(long payRollTransactionTypesNumber);
	Task<PayRollTransactionTypesView> GetViewById(long payRollTransactionTypesId);
	Task<PayRollTransactionTypesView> GetViewByNumber(long payRollTransactionTypesNumber);
}
