namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesOrder")]
    public partial class SalesOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalesOrder()
        {
            SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        public long SalesOrderId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Amount { get; set; }

        [StringLength(20)]
        public string OrderNumber { get; set; }

        [StringLength(10)]
        public string OrderType { get; set; }

        public long? CustomerAddressId { get; set; }

        public long? BillToAddressId { get; set; }

        public long? DeliveredToAddressId { get; set; }

        public long? ShippedToAddressId { get; set; }

        public long? InvoiceId { get; set; }

        [StringLength(10)]
        public string TakenBy { get; set; }

        [StringLength(10)]
        public string UnitOfMeasure { get; set; }

        public decimal? FreightAmount { get; set; }

        public long? CarrierAddressId { get; set; }

        public long? BuyerAddressId { get; set; }

        [StringLength(10)]
        public string PaymentInstrument { get; set; }

        [StringLength(10)]
        public string PaymentTerms { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TransactionDate { get; set; }

        public DateTime? ScheduledPickupDate { get; set; }

        public DateTime? ActualPickupDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
