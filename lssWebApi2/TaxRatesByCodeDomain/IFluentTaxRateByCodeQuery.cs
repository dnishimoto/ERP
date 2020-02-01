using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.TaxRatesByCodeDomain
{
    public interface IFluentTaxRatesByCodeQuery
    {
        Task<TaxRatesByCode> MapToEntity(TaxRatesByCodeView inputObject);
        Task<IList<TaxRatesByCode>> MapToEntity(IList<TaxRatesByCodeView> inputObjects);

        Task<TaxRatesByCodeView> MapToView(TaxRatesByCode inputObject);
        Task<NextNumber> GetNextNumber();
        Task<TaxRatesByCodeView> GetViewById(long ? taxRatesByCodeId);
        Task<TaxRatesByCodeView> GetViewByNumber(long taxRatesByCodeNumber);
        Task<TaxRatesByCodeView> GetViewByTaxCode(string code);
        Task<TaxRatesByCode> GetEntityByTaxCode(string code);
        Task<TaxRatesByCode> GetEntityById(long ? taxRatesByCodeId);
        Task<TaxRatesByCode> GetEntityByNumber(long taxRatesByCodeNumber);

    }
}
