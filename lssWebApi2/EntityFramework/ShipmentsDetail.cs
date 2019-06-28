using System;
using System.Collections.Generic;

namespace lssWebApi2.EntityFramework
{
    public partial class ShipmentsDetail
    {
        public long ShipmentDetailId { get; set; }
        public long ShipmentId { get; set; }
        public long ShipmentDetailNumber { get; set; }
        public long ItemId { get; set; }
        public long? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public long SalesOrderDetailId { get; set; }
        public long? QuantityShipped { get; set; }
        public decimal? AmountShipped { get; set; }
        public string Note { get; set; }

        public decimal? UnitPrice { get; set; }
        public decimal? Weight { get; set; }
        public string WeightUnitOfMeasure { get; set; }

        public decimal? Volume { get; set; }
        public string VolumeUnitOfMeasure { get; set; }
        public DateTime? ShippedDate { get; set; }

        public virtual ItemMaster Item { get; set; }
        public virtual Shipments Shipment { get; set; }

    }
}
