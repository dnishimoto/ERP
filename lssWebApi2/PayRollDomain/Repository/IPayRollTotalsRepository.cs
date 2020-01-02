

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
public interface IPayRollTotalsRepository
    {
        Task<PayRollTotals> GetEntityById(long _payRollTotalsId);
   
    }
}
