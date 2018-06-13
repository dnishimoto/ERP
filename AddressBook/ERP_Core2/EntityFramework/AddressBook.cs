namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AddressBook")]
    public partial class AddressBook
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AddressBook()
        {
            Employees = new HashSet<Employee>();
            Buyers = new HashSet<Buyer>();
            Carriers = new HashSet<Carrier>();
            Customers = new HashSet<Customer>();
            Emails = new HashSet<Email>();
            LocationAddresses = new HashSet<LocationAddress>();
            Phones = new HashSet<Phone>();
            Supervisors = new HashSet<Supervisor>();
            Suppliers = new HashSet<Supplier>();
        }

        [Key]
        public long AddressId { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public long? PeopleXrefId { get; set; }

        [StringLength(50)]
        public string CategoryCodeChar1 { get; set; }

        [StringLength(50)]
        public string CategoryCodeChar2 { get; set; }

        [StringLength(50)]
        public string CategoryCodeChar3 { get; set; }

        public int? CategoryCodeInt1 { get; set; }

        public int? CategoryCodeInt2 { get; set; }

        public int? CategoryCodeInt3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CategoryCodeDate1 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CategoryCodeDate2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CategoryCodeDate3 { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Buyer> Buyers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carrier> Carriers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Email> Emails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocationAddress> LocationAddresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phone> Phones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supervisor> Supervisors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
