namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Buyer")]
    public partial class Buyer
    {
        public long BuyerId { get; set; }

        public long AddressId { get; set; }

        public virtual AddressBook AddressBook { get; set; }
    }
}
