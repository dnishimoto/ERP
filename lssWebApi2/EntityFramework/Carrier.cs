using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Carrier
    {
        public long CarrierId { get; set; }
        public long AddressId { get; set; }
        public long CarrierTypeXrefId { get; set; }

        public AddressBook Address { get; set; }
        public Udc CarrierTypeXref { get; set; }
    }
}
