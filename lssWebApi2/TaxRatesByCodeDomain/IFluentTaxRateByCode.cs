using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.TaxRatesByCodeDomain
{
    public interface IFluentTaxRatesByCode
    {
        IFluentTaxRatesByCodeQuery Query();
        IFluentTaxRatesByCode Apply();
        IFluentTaxRatesByCode AddTaxRatesByCode(TaxRatesByCode TaxRatesByCode);
        IFluentTaxRatesByCode UpdateTaxRatesByCode(TaxRatesByCode TaxRatesByCode);
        IFluentTaxRatesByCode DeleteTaxRatesByCode(TaxRatesByCode TaxRatesByCode);
        IFluentTaxRatesByCode UpdateTaxRatesByCodes(IList<TaxRatesByCode> newObjects);
        IFluentTaxRatesByCode AddTaxRatesByCodes(List<TaxRatesByCode> newObjects);
        IFluentTaxRatesByCode DeleteTaxRatesByCodes(List<TaxRatesByCode> deleteObjects);
    }
}
