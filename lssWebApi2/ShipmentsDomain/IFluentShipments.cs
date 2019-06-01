
using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.ShipmentsDomain
{
public interface IFluentShipments
    {
        IFluentShipmentsQuery Query();
        IFluentShipments Apply();
        IFluentShipments AddShipments(Shipments shipments);
        IFluentShipments UpdateShipments(Shipments shipments);
        IFluentShipments DeleteShipments(Shipments shipments);
     	IFluentShipments UpdateShipmentss(List<Shipments> newObjects);
        IFluentShipments AddShipmentss(List<Shipments> newObjects);
        IFluentShipments DeleteAllShipments(List<Shipments> deleteObjects);
    }
}
