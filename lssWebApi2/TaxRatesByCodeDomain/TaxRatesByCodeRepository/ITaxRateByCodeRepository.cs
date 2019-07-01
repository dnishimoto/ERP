
using lssWebApi2.EntityFramework;
using System.Threading.Tasks;

namespace ERP_Core2.TaxRatesByCodeDomain
{
    public interface ITaxRatesByCodeRepository
    {
        Task<TaxRatesByCode> GetEntityById(long _TaxRatesByCodeId);
        Task<TaxRatesByCode> GetEntityByNumber(long TaxRatesByCodeNumber);

    }
}
