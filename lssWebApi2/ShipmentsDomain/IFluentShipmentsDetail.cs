using lssWebApi2.EntityFramework;
using System.Collections.Generic;

namespace lssWebApi2.ShipmentsDomain
{
    public interface IFluentShipmentsDetail
    {
        IFluentShipmentsDetailQuery Query();
        IFluentShipmentsDetail Apply();
        IFluentShipmentsDetail AddShipmentsDetail(ShipmentsDetail shipmentsDetail);
        IFluentShipmentsDetail UpdateShipmentsDetail(ShipmentsDetail shipmentsDetail);
        IFluentShipmentsDetail DeleteShipmentsDetail(ShipmentsDetail shipmentsDetail);
        IFluentShipmentsDetail UpdateShipmentsDetails(List<ShipmentsDetail> newObjects);
        IFluentShipmentsDetail AddShipmentsDetails(List<ShipmentsDetail> newObjects);
        IFluentShipmentsDetail DeleteShipmentsDetails(List<ShipmentsDetail> deleteObjects);
    }
}
