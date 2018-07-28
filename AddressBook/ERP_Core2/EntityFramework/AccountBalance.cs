namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AccountBalance")]
    public partial class AccountBalance
    {
        public long AccountBalanceId { get; set; }

        [StringLength(10)]
        public string AccountBalanceType { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public decimal? Hours { get; set; }

        public int FiscalYear { get; set; }

        public int FiscalPeriod { get; set; }

        public long AccountId { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }
    }
}
