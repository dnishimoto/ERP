using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
    public class ShipmentsModule
    {
        public FluentShipments Shipments = new FluentShipments();
        public FluentShipmentsDetail ShipmentsDetail = new FluentShipmentsDetail();
    }
}
