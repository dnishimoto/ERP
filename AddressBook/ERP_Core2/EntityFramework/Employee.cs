namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public long EmployeeId { get; set; }

        public long AddressId { get; set; }

        public long? PrimaryAddressId { get; set; }

        public long? PrimaryEmailId { get; set; }

        public long? PrimaryPhoneId { get; set; }

        public long? MailingAddressId { get; set; }

        [StringLength(50)]
        public string TaxIdentification { get; set; }

        public virtual AddressBook AddressBook { get; set; }
    }
}
