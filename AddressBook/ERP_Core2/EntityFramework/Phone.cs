namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Phone
    {
        public long PhoneId { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(10)]
        public string PhoneType { get; set; }

        [StringLength(10)]
        public string Extension { get; set; }

        public long AddressId { get; set; }

        public virtual AddressBook AddressBook { get; set; }
    }
}
