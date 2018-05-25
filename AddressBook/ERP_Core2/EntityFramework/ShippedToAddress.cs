namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ShippedToAddress
    {
        public long ShippedToAddressId { get; set; }

        public long AddressId { get; set; }

        [StringLength(100)]
        public string ShipToAddressLine1 { get; set; }

        [StringLength(100)]
        public string ShipToAddressLine2 { get; set; }

        [StringLength(50)]
        public string ShipToState { get; set; }

        [StringLength(50)]
        public string ShipToCity { get; set; }

        [StringLength(50)]
        public string ShipToZipcode { get; set; }

        public virtual AddressBook AddressBook { get; set; }
    }
}
