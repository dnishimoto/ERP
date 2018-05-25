namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("POQuote")]
    public partial class POQuote
    {
        public long Id { get; set; }

        [Column(TypeName = "money")]
        public decimal? QuoteAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SubmittedDate { get; set; }

        public long PoId { get; set; }

        public long DocNumber { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public long CustomerAddressId { get; set; }

        public long VendorAddressId { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual AddressBook AddressBook1 { get; set; }
    }
}
