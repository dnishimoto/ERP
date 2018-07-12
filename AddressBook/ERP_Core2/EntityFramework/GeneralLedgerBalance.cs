namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeneralLedgerBalance")]
    public partial class GeneralLedgerBalance
    {
        public long GeneralLedgerBalanceId { get; set; }

        public long AccountId { get; set; }

        [StringLength(2)]
        public string LedgerType { get; set; }

        public int? FiscalYear { get; set; }

        public int? FiscalPeriod { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public virtual ChartOfAcct ChartOfAcct { get; set; }
    }
}
