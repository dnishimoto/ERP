namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Email
    {
        public long EmailId { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        public bool? LoginEmail { get; set; }

        [Column("Email")]
        [Required]
        [StringLength(30)]
        public string Email1 { get; set; }

        public long AddressId { get; set; }

        public virtual AddressBook AddressBook { get; set; }
    }
}
