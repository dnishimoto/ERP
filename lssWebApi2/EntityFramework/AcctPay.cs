using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class AcctPay
    {
        public AcctPay()
        {
            SupplierLedger = new HashSet<SupplierLedger>();
        }

        public long AcctPayId { get; set; }
        public long? DocNumber { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string Remark { get; set; }
        public DateTime? Gldate { get; set; }
        public long SupplierId { get; set; }
        public long? ContractId { get; set; }
        public long? PoquoteId { get; set; }
        public string Description { get; set; }
        public long? PurchaseOrderId { get; set; }
        public decimal? Tax { get; set; }
        public long? InvoiceId { get; set; }
        public long AccountId { get; set; }
        public string DocType { get; set; }
        public string PaymentTerms { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? AmountOpen { get; set; }
        public string OrderNumber { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? AmountPaid { get; set; }

        public ChartOfAccts Account { get; set; }
        public Contract Contract { get; set; }
        public Invoice Invoice { get; set; }
        public Poquote Poquote { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<SupplierLedger> SupplierLedger { get; set; }
    }
}
