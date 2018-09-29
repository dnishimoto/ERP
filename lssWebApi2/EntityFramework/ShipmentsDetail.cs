using System;
using System.Collections.Generic;

namespace lssWebApi2.entityframework
{
    public partial class ShipmentsDetail
    {
        public long ShipmentDetailId { get; set; }
        public long ShipmentId { get; set; }
        public long ItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public long SalesOrderDetailId { get; set; }

        public ItemMaster Item { get; set; }
        public Shipments Shipment { get; set; }
    }
}
