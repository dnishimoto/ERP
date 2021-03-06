﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class SupplierInvoiceDetail
    {
        public long SupplierInvoiceDetailId { get; set; }
        public long SupplierInvoiceId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal? ExtendedCost { get; set; }
        public long ItemId { get; set; }
        public string Description { get; set; }
        public DateTime? DiscountDueDate { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public long SupplierInvoiceDetailNumber { get; set; }
        public long? InvoiceId { get; set; }
        public long? InvoiceDetailId { get; set; }

        public virtual ItemMaster Item { get; set; }
        public virtual SupplierInvoice SupplierInvoice { get; set; }

    }
}