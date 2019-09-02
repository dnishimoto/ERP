using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class AcctRec
    {
        public AcctRec()
        {
            AcctRecFee = new HashSet<AcctRecFee>();
            AcctRecInterest = new HashSet<AcctRecInterest>();
            CustomerLedger = new HashSet<CustomerLedger>();
        }

        public long AcctRecId { get; set; }
        public decimal? OpenAmount { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public DateTime? Gldate { get; set; }
        public long InvoiceId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? DocNumber { get; set; }
        public string Remarks { get; set; }
        public string PaymentTerms { get; set; }
        public long CustomerId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public string Description { get; set; }
        public long AcctRecDocTypeXrefId { get; set; }
        public long AccountId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string AcctRecDocType { get; set; }
        public decimal? InterestPaid { get; set; }
        public decimal? LateFee { get; set; }

        public virtual ChartOfAccts Account { get; set; }
        public virtual Udc AcctRecDocTypeXref { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual ICollection<AcctRecFee> AcctRecFee { get; set; }
        public virtual ICollection<AcctRecInterest> AcctRecInterest { get; set; }
        public virtual ICollection<CustomerLedger> CustomerLedger { get; set; }

    }
}