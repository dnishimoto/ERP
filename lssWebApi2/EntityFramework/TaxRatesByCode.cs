using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class TaxRatesByCode
    {
        public long TaxRatesByCodeId { get; set; }
        public string TaxCode { get; set; }
        public decimal? TaxRate { get; set; }
        public string State { get; set; }
        public long TaxRatesByCodeNumber { get; set; }

    }
}