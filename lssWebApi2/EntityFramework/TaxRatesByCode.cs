using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class TaxRatesByCode
    {
        public long TaxId { get; set; }
        public string TaxCode { get; set; }
        public decimal? TaxRate { get; set; }
        public string State { get; set; }
    }
}
