using lssWebApi2.AutoMapper;
using lssWebApi2.PayRollDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPayRollPaySequenceQuery
{
    Task<PayRollPaySequence> MapToEntity(PayRollPaySequenceView inputObject);
    Task<IList<PayRollPaySequence>> MapToEntity(IList<PayRollPaySequenceView> inputObjects);

    Task<PayRollPaySequenceView> MapToView(PayRollPaySequence inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PayRollPaySequence> GetEntityById(long payRollPaySequenceId);
    Task<PayRollPaySequence> GetEntityByNumber(long payRollPaySequenceNumber);
    Task<PayRollPaySequenceView> GetViewById(long payRollPaySequenceId);
    Task<PayRollPaySequenceView> GetViewByNumber(long payRollPaySequenceNumber);
    long GetMaxPaySequenceByGroupCode(long payRollGroupCode);
    Task<PayRollPaySequenceView> GetCurrentPaySequenceByGroupCode(long payRollGroupCode);
    Task<PayRollPaySequenceView> GetByDateRangeAndCode(
                    DateTime payRollBeginDate, DateTime payRollEndDate, int payRollGroupCode);
}
