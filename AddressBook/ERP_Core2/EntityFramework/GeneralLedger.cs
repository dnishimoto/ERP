namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralLedger")]
    public partial class GeneralLedger
    {
        public long GeneralLedgerId { get; set; }

        public long DocNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string DocType { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(10)]
        public string LedgerType { get; set; }

        public DateTime GLDate { get; set; }

        public long AccountId { get; set; }

        public DateTime CreatedDate { get; set; }

        public long AddressId { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? CreditAmount { get; set; }

        public int? FiscalYear { get; set; }

        public int? FiscalPeriod { get; set; }

        public virtual AddressBook AddressBook { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }

        public virtual CustomerLedger CustomerLedger { get; set; }
    }
}
