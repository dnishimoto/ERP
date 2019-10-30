

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollTransactionTypesRepository
    {
        Task<PayRollTransactionTypes> GetEntityById(long _payRollTransactionTypesId);
   
    }
}
