using lssWebApi2.AccountReceivableDomain;
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentAccountReceivableFeeQuery
{
    Task<AccountReceivableFee> MapToEntity(AccountReceivableFeeView inputObject);
    Task<IList<AccountReceivableFee>> MapToEntity(IList<AccountReceivableFeeView> inputObjects);
    Task<AccountReceivableFeeView> MapToView(AccountReceivableFee inputObject);
    Task<NextNumber> GetNextNumber();
    Task<AccountReceivableFee> GetEntityById(long ? accountReceivableFeeId);
    Task<AccountReceivableFee> GetEntityByNumber(long accountReceivableFeeNumber);
    Task<AccountReceivableFeeView> GetViewById(long ? accountReceivableFeeId);
    Task<AccountReceivableFeeView> GetViewByNumber(long accountReceivableFeeNumber);
}
