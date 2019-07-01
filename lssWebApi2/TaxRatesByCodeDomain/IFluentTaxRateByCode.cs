using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace ERP_Core2.TaxRatesByCodeDomain
{
    public interface IFluentTaxRatesByCode
    {
        IFluentTaxRatesByCodeQuery Query();
        IFluentTaxRatesByCode Apply();
        IFluentTaxRatesByCode AddTaxRatesByCode(TaxRatesByCode TaxRatesByCode);
        IFluentTaxRatesByCode UpdateTaxRatesByCode(TaxRatesByCode TaxRatesByCode);
        IFluentTaxRatesByCode DeleteTaxRatesByCode(TaxRatesByCode TaxRatesByCode);
        IFluentTaxRatesByCode UpdateTaxRatesByCodes(List<TaxRatesByCode> newObjects);
        IFluentTaxRatesByCode AddTaxRatesByCodes(List<TaxRatesByCode> newObjects);
        IFluentTaxRatesByCode DeleteTaxRatesByCodes(List<TaxRatesByCode> deleteObjects);
    }
}
