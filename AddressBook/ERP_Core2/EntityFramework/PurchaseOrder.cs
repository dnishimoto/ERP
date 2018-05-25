namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseOrder")]
    public partial class PurchaseOrder
    {
        public long PurchaseOrderId { get; set; }

        [StringLength(10)]
        public string POType { get; set; }

        [StringLength(10)]
        public string PaymentTerms { get; set; }

        [Column(TypeName = "money")]
        public decimal? GrossAmount { get; set; }

        public string Remark { get; set; }

        public DateTime? GLDate { get; set; }

        [StringLength(100)]
        public string AccountNumber { get; set; }

        public long? SupplierAddressId { get; set; }

        public long? CustomerAddressId { get; set; }

        public long? ContractId { get; set; }

        public long? POQuoteId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public long? ItemId { get; set; }

        [StringLength(50)]
        public string PONumber { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string UnitOfMeasure { get; set; }

        [StringLength(10)]
        public string TakenBy { get; set; }

        public long? ShippedToAddressId { get; set; }

        public long? BuyerAddressId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PromisedDeliveredDate { get; set; }

        public decimal? Tax { get; set; }

        [StringLength(10)]
        public string TaxCode { get; set; }
    }
}
