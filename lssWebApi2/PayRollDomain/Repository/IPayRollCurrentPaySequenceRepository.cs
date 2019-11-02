

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollCurrentPaySequenceRepository
    {
        Task<PayRollCurrentPaySequence> GetEntityById(long _payRollCurrentPaySequenceId);
        Task<PayRollCurrentPaySequence> GetViewByPayRollCode(long payRollGroupCode);


    }
}
