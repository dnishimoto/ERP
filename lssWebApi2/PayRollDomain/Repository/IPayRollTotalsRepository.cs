

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollTotalsRepository
    {
        Task<PayRollTotals> GetEntityById(long _payRollTotalsId);
   
    }
}
