using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollPaySequenceRepository
    {
        Task<PayRollPaySequence> GetEntityById(long _payRollPaySequenceId);
        long GetMaxSequenceNumber();
    }
}
