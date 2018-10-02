using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class LocationAddress
    {
        public LocationAddress()
        {
            ServiceInformation = new HashSet<ServiceInformation>();
        }

        public long LocationId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public long TypeXrefId { get; set; }
        public long AddressId { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual AddressBook Address { get; set; }
        public virtual Udc TypeXref { get; set; }
        public virtual ICollection<ServiceInformation> ServiceInformation { get; set; }
    }
}
