using lssWebApi2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.TaxRatesByCodeDomain
{
    public class TaxRatesByCodeModule
    {
        private UnitOfWork unitOfWork;
        public FluentTaxRatesByCode TaxRatesByCode;
        public TaxRatesByCodeModule()
        {
            unitOfWork = new UnitOfWork();
            TaxRatesByCode = new FluentTaxRatesByCode(unitOfWork);
        }
    }
}
