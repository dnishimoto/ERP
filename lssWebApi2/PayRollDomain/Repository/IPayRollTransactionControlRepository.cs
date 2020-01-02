

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public interface IPayRollTransactionControlRepository
    {
        Task<PayRollTransactionControl> GetEntityById(long _payRollTransactionControlId);
   
    }
}
