using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
    public interface IFluentShipmentsQuery
    {
        Task<Shipments> MapToEntity(ShipmentsView inputObject);
        Task<List<Shipments>> MapToEntity(List<ShipmentsView> inputObjects);
        Task<ShipmentsView> MapToView(Shipments inputObject);
        Task<NextNumber> GetNextNumber();
        Task<ShipmentsView> GetViewById(long shipmentId);
        Task<ShipmentsView> GetViewByNumber(long shipmentNumber);
    }
}
