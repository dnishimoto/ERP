using lssWebApi2.AccountPayableDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentAccountPayableQuery
{
    Task<AccountPayable> MapToEntity(AccountPayableView inputObject);
    Task<IList<AccountPayable>> MapToEntity(IList<AccountPayableView> inputObjects);
    Task<AccountPayableView> MapToView(AccountPayable inputObject);
    Task<NextNumber> GetNextNumber();
    Task<AccountPayable> GetEntityById(long ? accountPayableId);
    Task<AccountPayable> GetEntityByNumber(long accountPayableNumber);
    Task<AccountPayableView> GetViewById(long ? accountPayableId);
    Task<AccountPayableView> GetViewByNumber(long accountPayableNumber);
    Task<AccountPayable> GetAcctPayByPONumber(string poNumber);
    Task<long> GetNextDocNumber();
}
