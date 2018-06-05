namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Carrier")]
    public partial class Carrier
    {
        public long CarrierId { get; set; }

        public long AddressId { get; set; }

        public long CarrierTypeXrefId { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual UDC UDC { get; set; }
    }
}
