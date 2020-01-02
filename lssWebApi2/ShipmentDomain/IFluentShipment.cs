
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.ShipmentsDomain
{
public interface IFluentShipment
    {
        IFluentShipmentQuery Query();
        IFluentShipment UpdateShipment(Shipment shipments);
        IFluentShipment DeleteShipment(Shipment shipments);
     	IFluentShipment UpdateShipments(List<Shipment> newObjects);
        IFluentShipment AddShipments(List<Shipment> newObjects);
        IFluentShipment DeleteAllShipments(List<Shipment> deleteObjects);
        IFluentShipment AddShipment(Shipment shipments);
        IFluentShipment Apply();
    }
}
