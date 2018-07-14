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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocationAddress()
        {
            ServiceInformations = new HashSet<ServiceInformation>();
        }

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

        [StringLength(20)]
        public string Zipcode { get; set; }

        public long TypeXRefId { get; set; }

        public long AddressId { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceInformation> ServiceInformations { get; set; }

        public virtual UDC UDC { get; set; }
    }
}
