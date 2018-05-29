namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocationAddress")]
    public partial class LocationAddress
    {
        [Key]
        public long LocationId { get; set; }

        [Column("Address Line 1")]
        [StringLength(255)]
        public string Address_Line_1 { get; set; }

        [Column("Address Line 2")]
        [StringLength(255)]
        public string Address_Line_2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        public long StateXRefId { get; set; }

        [StringLength(20)]
        public string Zipcode { get; set; }

        public long CountryXRefId { get; set; }

        public long TypeXRefId { get; set; }

        public long AddressId { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual UDC UDC { get; set; }

        public virtual UDC UDC1 { get; set; }

        public virtual UDC UDC2 { get; set; }
    }
}
