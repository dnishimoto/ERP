namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcctPay")]
    public partial class AcctPay
    {
        public long AcctPayId { get; set; }

        public long? DocNumber { get; set; }

        public long DocTypeXRefId { get; set; }

        public long PaymentTermsXRefId { get; set; }

        [Column(TypeName = "money")]
        public decimal? GrossAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal? DiscountAmount { get; set; }

        public string Remark { get; set; }

        public DateTime? GLDate { get; set; }

        [StringLength(100)]
        public string AccountNumber { get; set; }

        public long SupplierId { get; set; }

        public long? ContractId { get; set; }

        public long? POQuoteId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public long? ItemId { get; set; }

        public long? PurchaseOrderId { get; set; }

        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }

        public long? InvoiceId { get; set; }

        public virtual UDC UDC { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual UDC UDC1 { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public virtual Contract Contract { get; set; }

        public virtual POQuote POQuote { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
