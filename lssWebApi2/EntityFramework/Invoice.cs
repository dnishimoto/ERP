using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class Invoice
    {
        public Invoice()
        {
            AcctRec = new HashSet<AcctRec>();
            CustomerLedger = new HashSet<CustomerLedger>();
            InvoiceDetail = new HashSet<InvoiceDetail>();
            ServiceInformationInvoice = new HashSet<ServiceInformationInvoice>();
        }

        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? Amount { get; set; }
        public long CustomerId { get; set; }
        public string Description { get; set; }
        public decimal? TaxAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public string PaymentTerms { get; set; }
        public long CompanyId { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? FreightCost { get; set; }
        public decimal? DiscountAmount { get; set; }

        public Company Company { get; set; }
        public Customer Customer { get; set; }
        public AcctPay AcctPay { get; set; }
        public ICollection<AcctRec> AcctRec { get; set; }
        public ICollection<CustomerLedger> CustomerLedger { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetail { get; set; }
        public ICollection<ServiceInformationInvoice> ServiceInformationInvoice { get; set; }
    }
}
