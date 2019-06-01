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
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public long SalesOrderDetailId { get; set; }

        public virtual ItemMaster Item { get; set; }
        public virtual Shipments Shipment { get; set; }

    }
}
