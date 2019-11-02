using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP_Core2.PayRollDomain;


public interface IFluentPayRollLedgerQuery
{
    Task<PayRollLedger> MapToEntity(PayRollLedgerView inputObject);
    Task<List<PayRollLedger>> MapToEntity(List<PayRollLedgerView> inputObjects);
    Task<PayRollLedgerView> MapToView(PayRollLedger inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PayRollLedger> GetEntityById(long payRollLedgerId);
    Task<PayRollLedger> GetEntityByNumber(long payRollLedgerNumber);
    Task<PayRollLedgerView> GetViewById(long payRollLedgerId);
    Task<PayRollLedgerView> GetViewByNumber(long payRollLedgerNumber);
    Task<List<PayRollLedger>> GetEntitiesByPaySequence(long employee, long paySequence);
}
