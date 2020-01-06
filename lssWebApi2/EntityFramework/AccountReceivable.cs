﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class AccountReceivable
    {
        public AccountReceivable()
        {
            AccountReceivableFee = new HashSet<AccountReceivableFee>();
            AccountReceivableInterest = new HashSet<AccountReceivableInterest>();
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
        public long AccountReceivableNumber { get; set; }

        public virtual ChartOfAccount Account { get; set; }
        public virtual Udc AcctRecDocTypeXref { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual ICollection<AccountReceivableFee> AccountReceivableFee { get; set; }
        public virtual ICollection<AccountReceivableInterest> AccountReceivableInterest { get; set; }
        public virtual ICollection<CustomerLedger> CustomerLedger { get; set; }

    }
}