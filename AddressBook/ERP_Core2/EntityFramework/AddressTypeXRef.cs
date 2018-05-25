namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AddressTypeXRef")]
    public partial class AddressTypeXRef
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string EntityType { get; set; }

        public long? AddressId { get; set; }
    }
}
