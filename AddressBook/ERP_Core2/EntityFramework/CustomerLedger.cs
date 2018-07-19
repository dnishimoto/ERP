namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerLedger")]
    public partial class CustomerLedger
    {
        public long CustomerLedgerId { get; set; }

        public long CustomerId { get; set; }

        public long InvoiceId { get; set; }

        public long AcctRecId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GLDate { get; set; }

        public long AccountId { get; set; }

        public long GeneralLedgerId { get; set; }

        public virtual AcctRec AcctRec { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
