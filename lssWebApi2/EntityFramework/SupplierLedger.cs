using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class SupplierLedger
    {
        public long SupplierLedgerId { get; set; }
        public long SupplierId { get; set; }
        public long InvoiceId { get; set; }
        public long AcctPayId { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Gldate { get; set; }
        public long AccountId { get; set; }
        public long GeneralLedgerId { get; set; }
        public long DocNumber { get; set; }
        public string Comment { get; set; }
        public long AddressId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string DocType { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public int FiscalYear { get; set; }
        public int FiscalPeriod { get; set; }

        public virtual AcctPay AcctPay { get; set; }
        public virtual GeneralLedger GeneralLedger { get; set; }
        public virtual SupplierInvoice Invoice { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
