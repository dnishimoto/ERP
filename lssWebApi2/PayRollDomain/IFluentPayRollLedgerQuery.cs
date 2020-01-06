using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using lssWebApi2.PayRollDomain;


public interface IFluentPayRollLedgerQuery
{
    Task<PayRollLedger> MapToEntity(PayRollLedgerView inputObject);
    Task<IList<PayRollLedger>> MapToEntity(IList<PayRollLedgerView> inputObjects);
    Task<PayRollLedgerView> MapToView(PayRollLedger inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PayRollLedger> GetEntityById(long payRollLedgerId);
    Task<PayRollLedger> GetEntityByNumber(long payRollLedgerNumber);
    Task<PayRollLedgerView> GetViewById(long payRollLedgerId);
    Task<PayRollLedgerView> GetViewByNumber(long payRollLedgerNumber);
    Task<IList<PayRollLedger>> GetEntitiesByPaySequence(long employee, long paySequence);
}
