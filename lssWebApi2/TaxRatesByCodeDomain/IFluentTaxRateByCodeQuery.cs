using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP_Core2.TaxRatesByCodeDomain
{
    public interface IFluentTaxRatesByCodeQuery
    {
        Task<TaxRatesByCode> MapToEntity(TaxRatesByCodeView inputObject);
        Task<List<TaxRatesByCode>> MapToEntity(List<TaxRatesByCodeView> inputObjects);

        Task<TaxRatesByCodeView> MapToView(TaxRatesByCode inputObject);
        Task<NextNumber> GetNextNumber();
        Task<TaxRatesByCodeView> GetViewById(long TaxRatesByCodeId);
        Task<TaxRatesByCodeView> GetViewByNumber(long TaxRatesByCodeNumber);
    }
}
