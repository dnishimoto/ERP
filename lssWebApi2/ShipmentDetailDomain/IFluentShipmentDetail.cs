using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.ShipmentsDomain
{
    public interface IFluentShipmentDetail
    {
        IFluentShipmentDetailQuery Query();
        IFluentShipmentDetail Apply();
        IFluentShipmentDetail AddShipmentDetail(ShipmentDetail shipmentsDetail);
        IFluentShipmentDetail UpdateShipmentDetail(ShipmentDetail shipmentsDetail);
        IFluentShipmentDetail DeleteShipmentDetail(ShipmentDetail shipmentsDetail);
        IFluentShipmentDetail UpdateShipmentDetails(IList<ShipmentDetail> newObjects);
        IFluentShipmentDetail AddShipmentDetails(List<ShipmentDetail> newObjects);
        IFluentShipmentDetail DeleteShipmentDetails(List<ShipmentDetail> deleteObjects);
    }
}
