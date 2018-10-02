using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Buyer
    {
        public long BuyerId { get; set; }
        public long AddressId { get; set; }
        public string Title { get; set; }

        public virtual AddressBook Address { get; set; }
    }
}
