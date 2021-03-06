﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class Shipment
    {
        public Shipment()
        {
            ShipmentDetail = new HashSet<ShipmentDetail>();
        }

        public long ShipmentId { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public long CustomerId { get; set; }
        public long CarrierId { get; set; }
        public string TrackingNumber { get; set; }
        public decimal? ActualWeight { get; set; }
        public decimal? BillableWeight { get; set; }
        public decimal? Duty { get; set; }
        public decimal? Tax { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CodAmount { get; set; }
        public long ShippedFromLocationId { get; set; }
        public long? ShippedToLocationId { get; set; }
        public long? PurchaseOrderId { get; set; }
        public long? VendorInvoiceId { get; set; }
        public decimal? VendorShippingCost { get; set; }
        public decimal? VendorHandlingCost { get; set; }
        public string OrderNumber { get; set; }
        public string OrderType { get; set; }
        public string WeightUom { get; set; }
        public long? SalesOrderId { get; set; }
        public long ShipmentNumber { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ShipmentDetail> ShipmentDetail { get; set; }

    }
}