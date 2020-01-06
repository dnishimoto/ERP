using lssWebApi2.AccountReceivableInterestDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentAccountReceivableInterestQuery
{
    Task<AccountReceivableInterest> MapToEntity(AccountReceivableInterestView inputObject);
    Task<IList<AccountReceivableInterest>> MapToEntity(IList<AccountReceivableInterestView> inputObjects);
    Task<AccountReceivableInterestView> MapToView(AccountReceivableInterest inputObject);
    Task<NextNumber> GetNextNumber();
    Task<AccountReceivableInterest> GetEntityById(long ? accountReceivableInterestId);
    Task<AccountReceivableInterest> GetEntityByNumber(long accountReceivableInterestNumber);
    Task<AccountReceivableInterestView> GetViewById(long ? accountReceivableInterestId);
    Task<AccountReceivableInterestView> GetViewByNumber(long accountReceivableInterestNumber);
}
