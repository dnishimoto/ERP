namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplierLedger")]
    public partial class SupplierLedger
    {
        public long SupplierLedgerId { get; set; }

        public long SupplierId { get; set; }

        public long InvoiceId { get; set; }

        public long AcctPayId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GLDate { get; set; }

        public long AccountId { get; set; }

        public long GeneralLedgerId { get; set; }

        public long DocNumber { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        public long AddressId { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string DocType { get; set; }

        [Column(TypeName = "money")]
        public decimal? DebitAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? CreditAmount { get; set; }

        public int FiscalYear { get; set; }

        public int FiscalPeriod { get; set; }

        public virtual AcctPay AcctPay { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual SupplierInvoice SupplierInvoice { get; set; }
    }
}
