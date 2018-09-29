using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ServiceInformationInvoice
    {
        public long ServiceInformationInvoiceId { get; set; }
        public long InvoiceId { get; set; }
        public long ServiceId { get; set; }

        public Invoice Invoice { get; set; }
        public ServiceInformation Service { get; set; }
    }
}
