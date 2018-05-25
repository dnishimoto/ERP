namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceInformation")]
    public partial class ServiceInformation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceInformation()
        {
            ScheduleEvents = new HashSet<ScheduleEvent>();
        }

        [Key]
        public long ServiceId { get; set; }

        [StringLength(255)]
        public string ServiceDescription { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        [StringLength(1000)]
        public string AddOns { get; set; }

        public long? ServiceTypeXRefId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? AddressId { get; set; }

        public long? ContractId { get; set; }

        public int? SquareFeetOfStructure { get; set; }

        [StringLength(255)]
        public string LocationDescription { get; set; }

        [StringLength(255)]
        public string LocationGPS { get; set; }

        [StringLength(1000)]
        public string Comments { get; set; }

        public bool Status { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual Contract Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleEvent> ScheduleEvents { get; set; }

        public virtual UDC UDC { get; set; }
    }
}
