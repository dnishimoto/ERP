using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Carrier
    {
        public long CarrierId { get; set; }
        public long AddressId { get; set; }
        public long CarrierTypeXrefId { get; set; }

        public virtual AddressBook Address { get; set; }
        public virtual Udc CarrierTypeXref { get; set; }

    }
}