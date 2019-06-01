using lssWebApi2.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{

    public interface IFluentShipmentsDetailQuery
    {
        Task<ShipmentsDetail> MapToEntity(ShipmentsDetailView inputObject);
        Task<List<ShipmentsDetail>> MapToEntity(List<ShipmentsDetailView> inputObjects);
        Task<List<ShipmentsDetail>> GetEntitiesByShipmentId(long shipmentId);
        Task<List<ShipmentsDetailView>> GetViewsByShipmentId(long shipmentId);

        Task<ShipmentsDetailView> MapToView(ShipmentsDetail inputObject);
        Task<NextNumber> GetNextNumber();
        Task<ShipmentsDetailView> GetViewById(long shipmentDetailId);
        Task<ShipmentsDetailView> GetViewByNumber(long shipmentDetailNumber);

        Task<ShipmentsDetail> GetEntityById(long shipmentDetailId);
        Task<ShipmentsDetail> GetEntityByNumber(long shipmentDetailNumber);
    }
}
