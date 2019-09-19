

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.PayRollDomain
{
public interface IPayRollGroupRepository
    {
        Task<PayRollGroup> GetEntityById(long _payRollGroupId);
   
    }
}
