using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class GeneralLedger
    {
        public GeneralLedger()
        {
            CustomerLedger = new HashSet<CustomerLedger>();
            SupplierLedger = new HashSet<SupplierLedger>();
        }

        public long GeneralLedgerId { get; set; }
        public long DocNumber { get; set; }
        public string DocType { get; set; }
        public decimal Amount { get; set; }
        public string LedgerType { get; set; }
        public DateTime Gldate { get; set; }
        public long AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long AddressId { get; set; }
        public string Comment { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public int? FiscalYear { get; set; }
        public int? FiscalPeriod { get; set; }
        public string CheckNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public decimal? Units { get; set; }

        public virtual ChartOfAccts Account { get; set; }
        public virtual AddressBook Address { get; set; }
        public virtual ICollection<CustomerLedger> CustomerLedger { get; set; }
        public virtual ICollection<SupplierLedger> SupplierLedger { get; set; }
    }
}
