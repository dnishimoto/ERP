using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class SupplierInvoice
    {
        public SupplierInvoice()
        {
            SupplierInvoiceDetail = new HashSet<SupplierInvoiceDetail>();
            SupplierLedger = new HashSet<SupplierLedger>();
        }

        public long SupplierInvoiceId { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public DateTime? SupplierInvoiceDate { get; set; }
        public string Ponumber { get; set; }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public decimal? TaxAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public long SupplierId { get; set; }
        public decimal? FreightCost { get; set; }
        public decimal? DiscountAmount { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<SupplierInvoiceDetail> SupplierInvoiceDetail { get; set; }
        public virtual ICollection<SupplierLedger> SupplierLedger { get; set; }
    }
}
