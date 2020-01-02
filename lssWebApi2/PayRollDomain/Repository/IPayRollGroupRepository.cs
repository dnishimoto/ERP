

using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace lssWebApi2.PayRollDomain
{
    public interface IPayRollGroupRepository
    {
        Task<PayRollGroup> GetEntityById(long _payRollGroupodeId);
        Task<PayRollGroup> GetEntityByNumber(long payRollGroupNumber);
        Task<PayRollGroup> GetEntityByCode(int code);
    }
}
