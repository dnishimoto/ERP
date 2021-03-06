﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Poquote
    {
        public Poquote()
        {
            AccountPayable = new HashSet<AccountPayable>();
        }

        public long PoquoteId { get; set; }
        public decimal? QuoteAmount { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public long PurchaseOrderId { get; set; }
        public string Remarks { get; set; }
        public long CustomerId { get; set; }
        public long SupplierId { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public long PoquoteNumber { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<AccountPayable> AccountPayable { get; set; }

    }
}