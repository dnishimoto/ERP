using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Phones
    {
        public long PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }
        public string Extension { get; set; }
        public long AddressId { get; set; }

        public virtual AddressBook Address { get; set; }

    }
}