using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ShippedToAddresses
    {
        public long ShippedToAddressId { get; set; }
        public long AddressId { get; set; }
        public string ShipToAddressLine1 { get; set; }
        public string ShipToAddressLine2 { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToZipcode { get; set; }
    }
}
