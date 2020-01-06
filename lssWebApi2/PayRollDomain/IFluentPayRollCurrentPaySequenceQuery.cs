using lssWebApi2.AutoMapper;
using lssWebApi2.PayRollDomain;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System.Collections.Generic;
using System.Threading.Tasks;


public interface IFluentPayRollCurrentPaySequenceQuery
{
    Task<PayRollCurrentPaySequence> MapToEntity(PayRollCurrentPaySequenceView inputObject);
    Task<IList<PayRollCurrentPaySequence>> MapToEntity(IList<PayRollCurrentPaySequenceView> inputObjects);

    Task<PayRollCurrentPaySequenceView> MapToView(PayRollCurrentPaySequence inputObject);
    Task<NextNumber> GetNextNumber();
    Task<PayRollCurrentPaySequence> GetEntityById(long payRollCurrentPaySequenceId);
    Task<PayRollCurrentPaySequence> GetEntityByNumber(long payRollCurrentPaySequenceNumber);
    Task<PayRollCurrentPaySequenceView> GetViewById(long payRollCurrentPaySequenceId);
    Task<PayRollCurrentPaySequenceView> GetViewByNumber(long payRollCurrentPaySequenceNumber);
    Task<PayRollCurrentPaySequenceView> GetViewByPayRollCode(long payRollGroupCode);
}
