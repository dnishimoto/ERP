using lssWebApi2.EntityFramework;
using System;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollPaySequenceRepository
    {
        Task<PayRollPaySequence> GetEntityById(long _payRollPaySequenceId);
        long GetMaxPaySequenceByGroupCode(long payRollGroupCode);
        Task<PayRollPaySequence> GetCurrentPaySequenceByGroupCode(long payRollGroupCode);
        Task<PayRollPaySequence> GetByDateRangeAndCode(DateTime payRollBeginDate, DateTime payRollEndDate, int payRollGroupCode);
    }
}
