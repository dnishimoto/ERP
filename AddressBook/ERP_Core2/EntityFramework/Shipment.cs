namespace ERP_Core2.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Shipment
    {
        public long ShipmentId { get; set; }

        public DateTime? ShipmentDate { get; set; }

        public long BillToAddressId { get; set; }

        public long CarrierId { get; set; }

        [StringLength(50)]
        public string TrackingNumber { get; set; }

        public decimal? ActualWeight { get; set; }

        public decimal? BillableWeight { get; set; }

        public decimal? Duty { get; set; }

        public decimal? Tax { get; set; }

        public decimal? ShippingCost { get; set; }

        public decimal? Amount { get; set; }

        public decimal? CodAmount { get; set; }

        public long ShippedFromId { get; set; }

        public long? ShippedToAddressId { get; set; }

        public long? PurchaseOrderId { get; set; }

        public long? VendorInvoiceId { get; set; }

        public decimal? VendorShippingCost { get; set; }

        public decimal? VendorHandlingCost { get; set; }
    }
}
