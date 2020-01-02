

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public interface IPayRollTransactionTypesRepository
    {
        Task<PayRollTransactionTypes> GetEntityById(long _payRollTransactionTypesId);
   
    }
}
