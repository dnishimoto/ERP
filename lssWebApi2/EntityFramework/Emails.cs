using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Emails
    {
        public long EmailId { get; set; }
        public string Password { get; set; }
        public bool? LoginEmail { get; set; }
        public string Email { get; set; }
        public long AddressId { get; set; }

        public AddressBook Address { get; set; }
    }
}
